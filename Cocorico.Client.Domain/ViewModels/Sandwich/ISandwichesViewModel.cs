using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Sandwich
{
    public interface ISandwichesViewModel
    {
        bool IsAdmin { get; }
        bool IsCustomer { get; }
        IReadOnlyList<SandwichDto> SandwichesList { get; }

        void AddToBasket(SandwichDto sandwich);
        void Edit(int sandwichId);
        Task DeleteAsync(int sandwichId);
        Task LoadSandwichesAsync();
    }
}
