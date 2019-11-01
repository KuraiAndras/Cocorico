using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.Ingredient
{
    public class IngredientsModel : ComponentBase
    {
        [Inject] private IIngredientClient IngredientClient { get; set; }

        [Inject] private NavigationManager UriHelper { get; set; }

        protected IReadOnlyList<IngredientDto> Ingredients { get; private set; } = new List<IngredientDto>();

        protected override async Task OnInitializedAsync() => await LoadIngredientsAsync();

        private async Task LoadIngredientsAsync()
        {
            try
            {
                var ingredients = await IngredientClient.GetAllAsync();

                Ingredients = ingredients.ToList();
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        protected void Edit(int id) => UriHelper.NavigateTo(Urls.Client.Ingredients + $"/{id}");

        protected async Task DeleteAsync(int id)
        {
            try
            {
                var fileResponse = await IngredientClient.DeleteAsync(id);

                if (fileResponse.IsSuccessfulStatusCode()) await LoadIngredientsAsync();
                //TODO: Handle fail
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }
    }
}
