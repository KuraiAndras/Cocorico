using Cocorico.Shared.Dtos.Order;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Order
{
    public interface IAddCustomerOrderViewModel
    {
        AddOrderDto AddOrderDto { get; }

        Task AddAsync();
        void DeleteSandwich(int id);
    }
}
