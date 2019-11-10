using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Sandwich
{
    public class AddSandwichViewModel : IAddSandwichViewModel
    {
        private readonly IIngredientClient _ingredientClient;
        private readonly ISandwichClient _sandwichClient;
        private readonly NavigationManager _navigationManager;

        public AddSandwichViewModel(
            IIngredientClient ingredientClient,
            ISandwichClient sandwichClient,
            NavigationManager navigationManager)
        {
            _ingredientClient = ingredientClient;
            _sandwichClient = sandwichClient;
            _navigationManager = navigationManager;

            NewSandwichDto = new SandwichAddDto { Ingredients = new List<IngredientDto>() };
            AvailableIngredients = new List<IngredientDto>();
            AddedIngredients = new List<IngredientDto>();
        }

        public SandwichAddDto NewSandwichDto { get; }
        public List<IngredientDto> AvailableIngredients { get; }
        public List<IngredientDto> AddedIngredients { get; }

        public async Task LoadAvailableIngredientsAsync()
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
            AddedIngredients.Add(ingredient);
            NewSandwichDto.Ingredients = AddedIngredients;
        }

        public void RemoveIngredient(IngredientDto ingredient)
        {
            AddedIngredients.Remove(ingredient);
            NewSandwichDto.Ingredients = AddedIngredients;
        }

        public async Task AddAsync()
        {
            try
            {
                NewSandwichDto.Ingredients = AddedIngredients;

                var result = await _sandwichClient.AddAsync(NewSandwichDto);

                if (result.IsSuccessfulStatusCode()) _navigationManager.NavigateTo(Urls.Client.Sandwiches);
                //TODO: Handle fail
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }
    }
}