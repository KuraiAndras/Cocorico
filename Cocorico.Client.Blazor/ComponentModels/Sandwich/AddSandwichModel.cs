using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.Sandwich
{
    public class AddSandwichModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUriHelper UriHelper { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private ISandwichClient SandwichHttpClient { get; set; }

        protected SandwichAddDto NewSandwichDto { get; } = new SandwichAddDto();

        protected async Task Add()
        {
            var result = await SandwichHttpClient.AddAsync(NewSandwichDto);

            if (result.IsSuccessfulStatusCode()) UriHelper.NavigateTo(Urls.Client.GetAllSandwich);
        }
    }
}