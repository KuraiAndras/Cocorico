using Cocorico.HttpClient;
using Cocorico.HttpClient.Extensions;
using Cocorico.Shared.Dtos.Ingredients;
using Cocorico.Shared.Dtos.Sandwiches;
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
            }
        }

        public void AddIngredient(IngredientDto ingredient)
        {
            AddedIngredients.Add(ingredient);
            NewSandwichDto.Ingredients = AddedIngredients;
        }

        public void RemoveIngredient(IngredientDto ingredient)
        {
            var ingredientToRemove = AddedIngredients.Single(i => i.Id == ingredient.Id);
            AddedIngredients.Remove(ingredientToRemove);
            NewSandwichDto.Ingredients = AddedIngredients;
        }

        public async Task<bool> TryAddAsync()
        {
            try
            {
                NewSandwichDto.Ingredients = AddedIngredients;

                var result = await _sandwichClient.AddAsync(NewSandwichDto);

                return result.IsSuccessfulStatusCode();
            }
            catch (SwaggerException)
            {
                return false;
            }
        }
    }
}
