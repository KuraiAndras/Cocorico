using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Services.Sandwich
{
    public interface ISandwichService
    {
        Task<SandwichResultDto> GetAsync(int id);
        Task<IEnumerable<SandwichResultDto>> GetAllAsync();
        Task AddOrUpdateAsync(NewSandwichDto newSandwichDto);
        Task DeleteAsync(int id);
    }
}
