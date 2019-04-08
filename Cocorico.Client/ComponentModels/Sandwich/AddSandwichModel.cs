using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cocorico.Client.ComponentModels.Sandwich
{
    public class AddSandwichModel : ComponentBase
    {
        [Inject] HttpClient HttpClient { get; set; }

        protected NewSandwichDto NewSandwichDto { get; set; } = new NewSandwichDto();

        protected async Task Add()
        {
            NewSandwichDto.Id = 0;
            var response = await HttpClient.PostAsync(Urls.Server.SandwichBase, new StringContent(Json.Serialize(NewSandwichDto), Encoding.UTF8, Verbs.ApplicationJson));

            //TODO: Handle fail
        }
    }
}