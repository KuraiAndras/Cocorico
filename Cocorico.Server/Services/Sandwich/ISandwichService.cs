using System.Collections.Generic;
using System.Threading.Tasks;
using Cocorico.Server.Model.Entities.Sandwich;

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
