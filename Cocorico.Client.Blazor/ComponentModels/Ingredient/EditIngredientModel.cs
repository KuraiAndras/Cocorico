using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.Ingredient
{
    public class EditIngredientModel : ComponentBase
    {
        [Parameter] public int Id { get; set; }

        [Inject] public NavigationManager UriHelper { get; set; }

        [Inject] public IIngredientClient IngredientClient { get; set; }

        protected IngredientDto IngredientDto { get; private set; } = new IngredientDto();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var ingredientDto = await IngredientClient.GetAsync(Id);

                IngredientDto = ingredientDto;
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        protected async Task Edit()
        {
            try
            {
                var fileResponse = await IngredientClient.UpdateAsync(IngredientDto);

                //TODO: Handle fail
                if (!fileResponse.IsSuccessfulStatusCode()) return;

                UriHelper.NavigateTo(Urls.Client.Ingredients);
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }
    }
}
