using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Shared.Services
{
    public interface ISandwichService
    {
        Task<IServiceResult<SandwichResultDto>> GetSandwichResult(int id);
        Task<IServiceResult<IEnumerable<SandwichResultDto>>> GetAllSandwichResultAsync();
        Task<IServiceResult> AddOrUpdateSandwichAsync(NewSandwichDto newSandwichDto);
        Task<IServiceResult> DeleteSandwichAsync(int id);
    }
}
