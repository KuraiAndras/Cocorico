﻿using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Client.Domain.Services.Basket;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Dtos.Sandwich;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cocorico.Client.Domain.ViewModels.Order
{
    public class AddCustomerOrderViewModel : IAddCustomerOrderViewModel
    {
        private readonly IBasketService _basketService;
        private readonly IOrderClient _orderClient;

        private readonly IIngredientClient _ingredientClient;
        private readonly ISandwichClient _sandwichClient;
        public SandwichDto Sandwich { get; private set; }
        private readonly List<IngredientDto> _addedIngredients;

        public AddOrderDto AddOrderDto { get; }
        public List<IngredientDto> AvailableIngredients { get; }

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

        public async Task AddAsync()
        {
            try
            {
                var result = await _orderClient.AddOrderAsync(AddOrderDto);

                //TODO: Go to orders
                if (result.IsSuccessfulStatusCode())
                {
                    _basketService.EmptyBasket();
                }
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
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
                //TODO: Handle fail
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

                //TODO: Handle fail
                if (!fileResponse.IsSuccessfulStatusCode()) return;
             }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }
    }
}