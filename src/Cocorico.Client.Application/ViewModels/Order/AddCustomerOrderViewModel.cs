﻿using Cocorico.Client.Application.Services.Basket;
using Cocorico.HttpClient;
using Cocorico.HttpClient.Extensions;
using Cocorico.Shared.Dtos.Ingredients;
using Cocorico.Shared.Dtos.Orders;
using Cocorico.Shared.Dtos.Sandwiches;
using Cocorico.Shared.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Application.ViewModels.Order
{
    public class AddCustomerOrderViewModel : IAddCustomerOrderViewModel
    {
        private readonly IBasketService _basketService;
        private readonly IOrderClient _orderClient;

        private readonly IIngredientClient _ingredientClient;
        private readonly ISandwichClient _sandwichClient;

        private readonly List<IngredientDto> _addedIngredients;

        public AddCustomerOrderViewModel(
            IBasketService basketService,
            IOrderClient orderClient,
            ISandwichClient sandwichClient,
            IIngredientClient ingredientClient)
        {
            _basketService = basketService;
            _orderClient = orderClient;
            AddOrderDto = new AddOrderDto { Sandwiches = _basketService.SandwichesInBasket };
            Sandwich = new SandwichDto();
            _addedIngredients = new List<IngredientDto>();
            _ingredientClient = ingredientClient;
            AvailableIngredients = new List<IngredientDto>();
            _sandwichClient = sandwichClient;
        }

        public AddOrderDto AddOrderDto { get; }
        public List<IngredientDto> AvailableIngredients { get; }
        public SandwichDto Sandwich { get; }

        public async Task AddAsync()
        {
            try
            {
                var result = await _orderClient.AddOrderAsync(AddOrderDto);

                if (result.IsSuccessfulStatusCode())
                {
                    _basketService.EmptyBasket();
                }
            }
            catch (SwaggerException)
            {
            }
        }

        public void DeleteSandwich(int id) => _basketService.RemoveFromBasket(id);

        public async Task LoadIngredientsAsync()
        {
            try
            {
                var ingredients = await _ingredientClient.GetAllAsync();
                AvailableIngredients.AddRange(ingredients);
            }
            catch (SwaggerException)
            {
            }
        }

        public void AddIngredient(IngredientDto ingredient)
        {
            _addedIngredients.Add(ingredient);
            Sandwich.Ingredients = _addedIngredients;
        }

        public void RemoveIngredient(IngredientDto ingredient)
        {
            _addedIngredients.Remove(ingredient);
            Sandwich.Ingredients = _addedIngredients;
        }

        public async Task EditAsync(SandwichDto sandwich)
        {
            try
            {
                Sandwich.Ingredients = _addedIngredients;

                var fileResponse = await _sandwichClient.UpdateAsync(sandwich);

                if (!fileResponse.IsSuccessfulStatusCode()) throw new UnexpectedException();
            }
            catch (SwaggerException)
            {
            }
        }
    }
}
