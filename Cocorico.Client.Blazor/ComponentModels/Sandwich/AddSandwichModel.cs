using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.Sandwich
{
    public class AddSandwichModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private NavigationManager UriHelper { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private ISandwichClient SandwichHttpClient { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IIngredientClient IngredientClient { get; set; }

        protected SandwichAddDto NewSandwichDto { get; } = new SandwichAddDto { Ingredients = new List<IngredientDto>() };

        protected List<IngredientDto> AvailableIngredients { get; private set; } = new List<IngredientDto>();

        private List<IngredientDto> AddedIngredients { get; } = new List<IngredientDto>();

        protected override async Task OnInitializedAsync()
        {
            try
            {
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
            NewSandwichDto.Ingredients = AddedIngredients;
        }

        protected void RemoveIngredient(IngredientDto ingredient)
        {
            AddedIngredients.Remove(ingredient);
            NewSandwichDto.Ingredients = AddedIngredients;
        }

        protected async Task Add()
        {
            try
            {
                NewSandwichDto.Ingredients = AddedIngredients;

                var result = await SandwichHttpClient.AddAsync(NewSandwichDto);

                if (result.IsSuccessfulStatusCode()) UriHelper.NavigateTo(Urls.Client.Sandwiches);
                //TODO: Handle fail
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }
    }
}