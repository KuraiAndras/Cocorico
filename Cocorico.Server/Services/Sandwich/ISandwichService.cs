using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Services.Sandwich
{
    public interface ISandwichService
    {
        Task<IServiceResult<SandwichResultDto>> GetAsync(int id);
        Task<IServiceResult<IEnumerable<SandwichResultDto>>> GetAllAsync();
        Task<IServiceResult> AddOrUpdateAsync(NewSandwichDto newSandwichDto);
        Task<IServiceResult> DeleteAsync(int id);
    }
}
