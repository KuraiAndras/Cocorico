﻿using Cocorico.Client.Extensions;
using Cocorico.Client.HttpClient;
using Cocorico.Shared.Api.Ingredients;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Ingredient
{
    public class EditIngredientViewModel : IEditIngredientViewModel
    {
        private readonly IIngredientClient _ingredientClient;

        public EditIngredientViewModel(IIngredientClient ingredientClient)
        {
            _ingredientClient = ingredientClient;
            IngredientDto = new IngredientDto();
        }

        public IngredientDto IngredientDto { get; private set; }

        public async Task LoadIngredientAsync(int id)
        {
            try
            {
                IngredientDto = await _ingredientClient.GetAsync(id);
            }
            catch (SwaggerException)
            {
            }
        }

        public async Task<bool> EditIngredientAsync()
        {
            try
            {
                var fileResponse = await _ingredientClient.UpdateAsync(IngredientDto);

                return fileResponse.IsSuccessfulStatusCode();
            }
            catch (SwaggerException)
            {
                return false;
            }
        }
    }
}
