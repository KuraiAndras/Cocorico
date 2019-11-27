using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.SandwichService
{
    public interface IServerSandwichService
    {
        Task<SandwichDto> GetAsync(int id);
        Task<ICollection<SandwichDto>> GetAllAsync();
        Task AddAsync(SandwichAddDto sandwichAddDto);
        Task UpdateAsync(SandwichDto sandwichDto);
        Task DeleteAsync(int id);
    }
}
