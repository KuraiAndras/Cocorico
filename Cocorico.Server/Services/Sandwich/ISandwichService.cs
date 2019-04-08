using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cocorico.Server.Helpers;

namespace Cocorico.Server.Services.Sandwich
{
    public interface ISandwichService
    {
        Task<ServiceResult<SandwichResultDto>> GetAsync(int id);
        Task<ServiceResult<IEnumerable<SandwichResultDto>>> GetAllAsync();
        Task<ServiceResult> AddOrUpdateAsync(NewSandwichDto newSandwichDto);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
