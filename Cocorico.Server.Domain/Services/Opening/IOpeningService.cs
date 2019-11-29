using Cocorico.Shared.Dtos.Opening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.Opening
{
    public interface IOpeningService
    {
        Task<ICollection<OpeningDto>> GetAllOpeningsAsync();
        Task AddOpening(AddOpeningDto addOpeningDto);
        Task UpdateOpening(OpeningDto openingDto);
        Task DeleteOpening(int openingId);
        Task<bool> CanAddOrderAsync(DateTime addTime);
    }
}
