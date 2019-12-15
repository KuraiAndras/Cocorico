using Cocorico.Application.Common.Persistence;
using Cocorico.Application.Orders.Services.Price;
using Cocorico.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Orders.Queries.CalculatePrice
{
    public sealed class CalculatePriceQueryHandler : IRequestHandler<CalculatePriceQuery, int>
    {
        private readonly ICocoricoDbContext _context;
        private readonly IPriceCalculator _priceCalculator;

        public CalculatePriceQueryHandler(
            ICocoricoDbContext context,
            IPriceCalculator priceCalculator)
        {
            _context = context;
            _priceCalculator = priceCalculator;
        }

        public async Task<int> Handle(CalculatePriceQuery request, CancellationToken cancellationToken)
        {
            var sandwichesInDb = await _context
                .Sandwiches
                .AsNoTracking()
                .Include(s => s.SandwichIngredients)
                .AsNoTracking()
                .Include(s => s.SandwichIngredients)
                .ThenInclude(si => si.Ingredient)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var sandwichesFromOrderInDb = request.Dto.Sandwiches
                .Select(sandwichDto => sandwichesInDb.SingleOrDefault(s => s.Id == sandwichDto.Id))
                .Where(s => !(s is null))
                .ToList();

            if (sandwichesFromOrderInDb.Count != request.Dto.Sandwiches.Count) throw new InvalidOperationException("Some sandwiches do not exist in database");

            if (request.Dto.SandwichModifications.Count == 0) return sandwichesFromOrderInDb.Select(s => s.Price).Aggregate((sum, price) => sum + price);

            const int ingredientPrice = 50;
            return request.Dto.SandwichModifications
                .Select(kvp =>
                {
                    var (currentSandwich, ingredientModificationDtos) = kvp;

                    var currentSandwichFromDb = sandwichesFromOrderInDb.First(s => s.Id == currentSandwich.Id);

                    var removedIngredients = ingredientModificationDtos.Where(imd => imd.Modification == Modifier.Remove).ToList();
                    if (!removedIngredients.All(ri => currentSandwichFromDb.SandwichIngredients.Select(si => si.Ingredient).Any(i => ri.IngredientId == i.Id)))
                    {
                        throw new InvalidOperationException($"Removed some ingredient from sandwich: {currentSandwichFromDb.Name} which is originally not on it");
                    }

                    var addedIngredients = ingredientModificationDtos.Where(imd => imd.Modification == Modifier.Add).ToList();
                    if (!addedIngredients.All(ai => currentSandwichFromDb.SandwichIngredients.Select(si => si.Ingredient).Any(i => ai.IngredientId != i.Id)))
                    {
                        throw new InvalidOperationException($"Added some ingredient to sandwich: {currentSandwichFromDb.Name} which is already on it");
                    }

                    var basePrice = currentSandwichFromDb.Price;

                    return _priceCalculator.CalculatePrice(basePrice, addedIngredients.Count, removedIngredients.Count, ingredientPrice);
                })
                .Aggregate((sum, price) => sum + price);
        }
    }
}