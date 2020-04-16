using Cocorico.Shared.Dtos.Sandwiches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Sandwich
{
    public interface ISandwichesViewModel
    {
        IReadOnlyList<SandwichDto> SandwichesList { get; }

        void AddToBasket(SandwichDto sandwich);
        Task DeleteAsync(int sandwichId);
        Task LoadSandwichesAsync();
    }
}
