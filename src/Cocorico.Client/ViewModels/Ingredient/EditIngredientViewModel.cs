using AutoMapper;
using Cocorico.Client.Extensions;
using Cocorico.Client.HttpClient;
using Cocorico.Shared.Api.Ingredients;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Ingredient
{
    public class EditIngredientViewModel : IEditIngredientViewModel
    {
        private readonly IIngredientClient _ingredientClient;
        private readonly IMapper _mapper;

        public EditIngredientViewModel(
            IIngredientClient ingredientClient,
            IMapper mapper)
        {
            _ingredientClient = ingredientClient;
            _mapper = mapper;
            IngredientDto = new UpdateIngredient();
        }

        public UpdateIngredient IngredientDto { get; private set; }

        public async Task LoadIngredientAsync(int id)
        {
            try
            {
                var ingredient = await _ingredientClient.GetAsync(id);
                IngredientDto = _mapper.Map<UpdateIngredient>(ingredient);
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
