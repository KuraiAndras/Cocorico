using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Application.ViewModels.Sandwich
{
    public interface ISandwichesViewModel
    {
        IReadOnlyList<SandwichDto> SandwichesList { get; }

        void AddToBasket(SandwichDto sandwich);
        Task DeleteAsync(int sandwichId);
        Task LoadSandwichesAsync();
    }
}
