using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.Ingredient
{
    public class AddIngredientModel : ComponentBase
    {
        [Inject] public NavigationManager UriHelper { get; set; }

        [Inject] public IIngredientClient IngredientClient { get; set; }

        protected IngredientAddDto IngredientAddDto { get; set; } = new IngredientAddDto();

        protected async Task Add()
        {
            var result = await IngredientClient.AddAsync(IngredientAddDto);

            if (result.IsSuccessfulStatusCode()) UriHelper.NavigateTo(Urls.Client.Ingredients);
        }
    }
}
