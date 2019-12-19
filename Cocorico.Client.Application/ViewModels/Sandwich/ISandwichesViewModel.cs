using System.Collections.Generic;
using System.Threading.Tasks;
using Cocorico.Shared.Dtos.Sandwich;

namespace Cocorico.Client.Application.ViewModels.Sandwich
{
    public interface ISandwichesViewModel
    {
        IReadOnlyList<SandwichDto> SandwichesList { get; }

        void AddToBasket(SandwichDto sandwich);
        void Edit(int sandwichId);
        Task DeleteAsync(int sandwichId);
        Task LoadSandwichesAsync();
    }
}
