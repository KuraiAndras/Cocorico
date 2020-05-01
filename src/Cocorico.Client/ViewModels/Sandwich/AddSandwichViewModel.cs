using Cocorico.Client.Extensions;
using Cocorico.Client.HttpClient;
using Cocorico.Shared.Api.Ingredients;
using Cocorico.Shared.Api.Sandwiches;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Sandwich
{
    public class AddSandwichViewModel : IAddSandwichViewModel
    {
        private readonly IIngredientClient _ingredientClient;
        private readonly ISandwichClient _sandwichClient;

        public AddSandwichViewModel(
            IIngredientClient ingredientClient,
            ISandwichClient sandwichClient)
        {
            _ingredientClient = ingredientClient;
            _sandwichClient = sandwichClient;

            NewAddSandwichDto = new AddSandwich { Ingredients = new List<IngredientDto>() };
            AvailableIngredients = new List<IngredientDto>();
            AddedIngredients = new List<IngredientDto>();
        }

        public AddSandwich NewAddSandwichDto { get; }
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
            }
        }

        public void AddIngredient(IngredientDto ingredient)
        {
            AddedIngredients.Add(ingredient);
            NewAddSandwichDto.Ingredients = AddedIngredients;
        }

        public void RemoveIngredient(IngredientDto ingredient)
        {
            var ingredientToRemove = AddedIngredients.Single(i => i.Id == ingredient.Id);
            AddedIngredients.Remove(ingredientToRemove);
            NewAddSandwichDto.Ingredients = AddedIngredients;
        }

        public async Task<bool> TryAddAsync()
        {
            try
            {
                NewAddSandwichDto.Ingredients = AddedIngredients;

                var result = await _sandwichClient.AddAsync(NewAddSandwichDto);

                return result.IsSuccessfulStatusCode();
            }
            catch (SwaggerException)
            {
                return false;
            }
        }
    }
}
