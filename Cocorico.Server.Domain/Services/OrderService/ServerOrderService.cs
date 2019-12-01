using AutoMapper;
using Cocorico.DAL.Models;
using Cocorico.DAL.Models.Entities;
using Cocorico.Server.Domain.Services.Opening;
using Cocorico.Server.Domain.Services.ServiceBase;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Price;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.OrderService
{
    public class ServerOrderService : EntityServiceBase<Order>, IServerOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRotatingIdService _idService;
        private readonly IOpeningService _openingService;
        private readonly IPriceCalculator _priceCalculator;

        public ServerOrderService(
            CocoricoDbContext context,
            IMapper mapper,
            IOrderRotatingIdService idService,
            IOpeningService openingService,
            IPriceCalculator priceCalculator) : base(context)
        {
            _mapper = mapper;
            _idService = idService;
            _openingService = openingService;
            _priceCalculator = priceCalculator;
        }

        public async Task<ICollection<CustomerViewOrderDto>> GetAllOrderForCustomerAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId)) throw new EntityNotFoundException($"Invalid customer Id:{customerId}");

            var ordersForCustomer = await Context.Orders
                                        .Include(o => o.SandwichOrders)
                                        .ThenInclude(sl => sl.Sandwich)
                                        .ThenInclude(s => s.SandwichIngredients)
                                        .ThenInclude(il => il.Ingredient)
                                        .Include(o => o.SandwichOrders)
                                        .ThenInclude(so => so.IngredientModifications)
                                        .ThenInclude(im => im.Ingredient)
                                        .Where(o => o.CocoricoUserId == customerId)
                                        .ToListAsync()
                                    ?? throw new UnexpectedException();

            return ordersForCustomer.Select(order => _mapper.Map<CustomerViewOrderDto>(order)).ToList();
        }

        public async Task<ICollection<WorkerOrderViewDto>> GetPendingOrdersForWorkerAsync()
        {
            var ordersForWorkerView = await Context.Orders
                                          .Include(o => o.SandwichOrders)
                                          .ThenInclude(sl => sl.Sandwich)
                                          .ThenInclude(s => s.SandwichIngredients)
                                          .ThenInclude(il => il.Ingredient)
                                          .Include(o => o.SandwichOrders)
                                          .ThenInclude(so => so.IngredientModifications)
                                          .ThenInclude(im => im.Ingredient)
                                          .Include(o => o.CocoricoUser)
                                          .Where(o => o.State != OrderState.Delivered && o.State != OrderState.Rejected)
                                          .ToListAsync() ?? throw new UnexpectedException();

            return ordersForWorkerView.Select(order => _mapper.Map<WorkerOrderViewDto>(order)).ToList();
        }

        public async Task UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            var order = await Context.Orders
                            .SingleOrDefaultAsync(o => o.Id == updateOrderDto.OrderId)
                        ?? throw new EntityNotFoundException($"Order not found with id:{updateOrderDto.OrderId}");

            order.State = updateOrderDto.State;

            await UpdateAsync(order);
        }

        public async Task<int> AddOrderAsync(AddOrderDto addOrderDto)
        {
            var dateAdded = DateTime.Now;
            if (!await _openingService.CanAddOrderAsync(dateAdded)) throw new StoreClosedException();

            var sandwichesInDb = await Context
                .Sandwiches
                .ToListAsync();

            var sandwichesFromOrderInDb = addOrderDto.Sandwiches
                .Select(sandwichDto => sandwichesInDb.SingleOrDefault(s => s.Id == sandwichDto.Id))
                .Where(s => !(s is null))
                .ToList();

            if (sandwichesFromOrderInDb.Count == 0) throw new EntityNotFoundException();

            var orderPrice = await CalculatePriceAsync(addOrderDto);

            var currentOpening = await Context.Openings.FirstAsync(o => o.Start <= dateAdded && o.End > dateAdded);

            var newOrder = new Order
            {
                CocoricoUserId = addOrderDto.CustomerId,
                Price = orderPrice,
                State = OrderState.OrderPlaced,
                SandwichOrders = new List<SandwichOrder>(),
                RotatingId = _idService.GetNextId(),
                Time = dateAdded,
                Opening = currentOpening,
            };

            foreach (var sandwich in sandwichesFromOrderInDb)
            {
                newOrder.SandwichOrders.Add(new SandwichOrder
                {
                    Order = newOrder,
                    Sandwich = sandwich,
                    IngredientModifications = _mapper.Map<ICollection<IngredientModification>>(
                        addOrderDto.SandwichModifications.SingleOrDefault(kvp => kvp.Key.Id == sandwich.Id).Value
                        ?? new List<IngredientModificationDto>()),
                });
            }

            await Context.Orders.AddAsync(newOrder);

            await Context.SaveChangesAsync();

            return newOrder.Id;
        }

        public async Task<int> CalculatePriceAsync(AddOrderDto addOrderDto)
        {
            var sandwichesInDb = await Context
                .Sandwiches
                .AsNoTracking()
                .Include(s => s.SandwichIngredients)
                .AsNoTracking()
                .Include(s => s.SandwichIngredients)
                .ThenInclude(si => si.Ingredient)
                .AsNoTracking()
                .ToListAsync();

            var sandwichesFromOrderInDb = addOrderDto.Sandwiches
                .Select(sandwichDto => sandwichesInDb.SingleOrDefault(s => s.Id == sandwichDto.Id))
                .Where(s => !(s is null))
                .ToList();

            if (sandwichesFromOrderInDb.Count != addOrderDto.Sandwiches.Count) throw new InvalidOperationException("Some sandwiches do not exist in database");

            if (addOrderDto.SandwichModifications.Count == 0) return sandwichesFromOrderInDb.Select(s => s.Price).Aggregate((sum, price) => sum + price);

            const int ingredientPrice = 50;
            return addOrderDto.SandwichModifications
                .Select(kvp =>
                {
                    var (currentSandwich, ingredientModificationDtos) = kvp;

                    var currentSandwichFromDb = sandwichesFromOrderInDb.First(s => s.Id == currentSandwich.Id);

                    var removedIngredients = ingredientModificationDtos.Where(imd => imd.Modification == Modifier.Remove).ToList();
                    if (!removedIngredients.All(ri => currentSandwichFromDb.Ingredients().Any(i => ri.IngredientId == i.Id)))
                    {
                        throw new InvalidOperationException($"Removed some ingredient from sandwich: {currentSandwichFromDb.Name} which is originally not on it");
                    }

                    var addedIngredients = ingredientModificationDtos.Where(imd => imd.Modification == Modifier.Add).ToList();
                    if (!addedIngredients.All(ai => currentSandwichFromDb.Ingredients().Any(i => ai.IngredientId != i.Id)))
                    {
                        throw new InvalidOperationException($"Added some ingredient to sandwich: {currentSandwichFromDb.Name} which is already on it");
                    }

                    var basePrice = currentSandwichFromDb.Price;

                    return _priceCalculator.CalculatePrice(basePrice, addedIngredients.Count, removedIngredients.Count, ingredientPrice);
                })
                .Aggregate((sum, price) => sum + price);
        }

        public async Task DeleteOrderAsync(int orderId) => await DeleteByIdAsync(orderId);
    }
}
