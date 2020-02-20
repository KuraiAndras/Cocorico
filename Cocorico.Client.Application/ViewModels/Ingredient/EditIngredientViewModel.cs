﻿using Cocorico.HttpClient;
using Cocorico.HttpClient.Extensions;
using Cocorico.Shared.Dtos.Ingredient;
using System.Threading.Tasks;

namespace Cocorico.Client.Application.ViewModels.Ingredient
{
    public class EditIngredientViewModel : IEditIngredientViewModel
    {
        private readonly IIngredientClient _ingredientClient;

        public IngredientDto IngredientDto { get; private set; }

        public EditIngredientViewModel(IIngredientClient ingredientClient)
        {
            _ingredientClient = ingredientClient;
            IngredientDto = new IngredientDto();
        }

        public async Task LoadIngredientAsync(int id)
        {
            try
            {
                IngredientDto = await _ingredientClient.GetAsync(id);
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
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