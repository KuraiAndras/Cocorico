using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Cocorico.Client.ComponentModels.Sandwich
{
    public class AddSandwichModel : ComponentBase
    {
        [Inject] HttpClient HttpClient { get; set; }

        protected NewSandwichDto NewSandwichDto { get; } = new NewSandwichDto();

        protected async Task Add()
        {
            NewSandwichDto.Id = 0;
            await HttpClient.PostAsync(Urls.Server.SandwichBase, new StringContent(Json.Serialize(NewSandwichDto), Encoding.UTF8, Verbs.ApplicationJson));

            //TODO: Handle fail
        }
    }
}