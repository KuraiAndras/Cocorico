using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Shared.Services
{
    public interface ISandwichService
    {
        Task<SandwichDto> GetSandwichResultAsync(int id);
        Task<IEnumerable<SandwichDto>> GetAllSandwichResultAsync();

        Task AddSandwichAsync(SandwichAddDto newSandwichDto);
        Task UpdateSandwichAsync(SandwichDto sandwichDto);

        Task DeleteSandwichAsync(int id);
    }
}
