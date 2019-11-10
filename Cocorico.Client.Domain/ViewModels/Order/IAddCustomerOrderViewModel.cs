using Cocorico.Shared.Dtos.Order;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Order
{
    public interface IAddCustomerOrderViewModel
    {
        OrderAddDto OrderAddDto { get; }

        Task AddAsync();
        void DeleteSandwich(int id);
    }
}
