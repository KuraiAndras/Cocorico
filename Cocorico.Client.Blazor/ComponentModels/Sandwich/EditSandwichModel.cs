using System.Collections.Generic;
using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Cocorico.Shared.Dtos.Ingredient;

namespace Cocorico.Client.Blazor.ComponentModels.Sandwich
{
    public class EditSandwichModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Parameter] public int Id { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private NavigationManager UriHelper { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private ISandwichClient SandwichHttpClient { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IIngredientClient IngredientClient { get; set; }

        protected SandwichDto Sandwich { get; private set; } = new SandwichDto();

        protected List<IngredientDto> AvailableIngredients { get; private set; } = new List<IngredientDto>();

        private List<IngredientDto> AddedIngredients { get; } = new List<IngredientDto>();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var sandwichDto = await SandwichHttpClient.GetAsync(Id);
                Sandwich = sandwichDto;
                AddedIngredients.Clear();
                AddedIngredients.AddRange(sandwichDto.Ingredients);

                var ingredients = await IngredientClient.GetAllAsync();

                AvailableIngredients.AddRange(ingredients);
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        protected void AddIngredient(IngredientDto ingredient)
        {
            AddedIngredients.Add(ingredient);
            Sandwich.Ingredients = AddedIngredients;
        }

        protected void RemoveIngredient(IngredientDto ingredient)
        {
            AddedIngredients.Remove(ingredient);
            Sandwich.Ingredients = AddedIngredients;
        }

        protected async Task Edit()
        {
            try
            {
                Sandwich.Ingredients = AddedIngredients;

                var fileResponse = await SandwichHttpClient.UpdateAsync(Sandwich);

                //TODO: Handle fail
                if (!fileResponse.IsSuccessfulStatusCode()) return;

                UriHelper.NavigateTo(Urls.Client.Sandwiches);
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }
    }
}
