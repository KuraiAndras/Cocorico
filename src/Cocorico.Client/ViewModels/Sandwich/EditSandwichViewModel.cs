using AutoMapper;
using Cocorico.Client.Extensions;
using Cocorico.Client.HttpClient;
using Cocorico.Shared.Api.Ingredients;
using Cocorico.Shared.Api.Sandwiches;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Sandwich
{
    public class EditSandwichViewModel : IEditSandwichViewModel
    {
        private readonly ISandwichClient _sandwichClient;
        private readonly IIngredientClient _ingredientClient;
        private readonly IMapper _mapper;
        private readonly List<IngredientDto> _addedIngredients;

        public EditSandwichViewModel(
            ISandwichClient sandwichClient,
            IIngredientClient ingredientClient,
            IMapper mapper)
        {
            _sandwichClient = sandwichClient;
            _ingredientClient = ingredientClient;
            _mapper = mapper;

            _addedIngredients = new List<IngredientDto>();

            Sandwich = new UpdateSandwich();
            AvailableIngredients = new List<IngredientDto>();
        }

        public UpdateSandwich Sandwich { get; private set; }
        public List<IngredientDto> AvailableIngredients { get; }

        public async Task LoadIngredientsAsync(int id)
        {
            try
            {
                var sandwichDto = await _sandwichClient.GetAsync(id);
                Sandwich = _mapper.Map<UpdateSandwich>(sandwichDto);
                _addedIngredients.Clear();
                _addedIngredients.AddRange(Sandwich.Ingredients);

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
            var ingredientToRemove = _addedIngredients.Single(i => i.Id == ingredient.Id);
            _addedIngredients.Remove(ingredientToRemove);
            Sandwich.Ingredients = _addedIngredients;
        }

        public async Task<bool> TryEditAsync()
        {
            try
            {
                Sandwich.Ingredients = _addedIngredients;

                var fileResponse = await _sandwichClient.UpdateAsync(Sandwich);

                return fileResponse.IsSuccessfulStatusCode();
            }
            catch (SwaggerException)
            {
                return false;
            }
        }
    }
}
