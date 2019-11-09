using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Ingredient
{
    public class EditIngredientViewModel : IEditIngredientViewModel
    {
        private readonly NavigationManager _navigationManager;
        private readonly IIngredientClient _ingredientClient;

        public IngredientDto IngredientDto { get; private set; }

        public EditIngredientViewModel(NavigationManager navigationManager, IIngredientClient ingredientClient)
        {
            _navigationManager = navigationManager;
            _ingredientClient = ingredientClient;
            IngredientDto = new IngredientDto();
        }

        public async Task LoadIngredientAsync(int id)
        {
            try
            {
                var ingredientDto = await _ingredientClient.GetAsync(id);

                IngredientDto = ingredientDto;
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        public async Task EditIngredientAsync()
        {
            try
            {
                var fileResponse = await _ingredientClient.UpdateAsync(IngredientDto);

                //TODO: Handle fail
                if (!fileResponse.IsSuccessfulStatusCode()) return;

                _navigationManager.NavigateTo(Urls.Client.Ingredients);
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }
    }
}