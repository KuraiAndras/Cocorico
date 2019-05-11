using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Shared.Services
{
    public interface ISandwichService
    {
        Task<SandwichResultDto> GetSandwichResultAsync(int id);
        Task<IEnumerable<SandwichResultDto>> GetAllSandwichResultAsync();
        Task AddOrUpdateSandwichAsync(NewSandwichDto newSandwichDto);
        Task DeleteSandwichAsync(int id);
    }
}
