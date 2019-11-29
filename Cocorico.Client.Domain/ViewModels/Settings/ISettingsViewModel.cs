using Cocorico.Shared.Dtos.Opening;
using Cocorico.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Settings
{
    public interface ISettingsViewModel
    {
        MutableRange IdRange { get; set; }
        ICollection<OpeningDto> Openings { get; }
        AddOpeningDto OpeningToAdd { get; set; }

        Task InitializeAsync();
        Task SetNewRangeAsync();
        Task AddOpeningAsync();
        Task EditOpeningAsync(Shared.Dtos.Opening.OpeningDto opening);
        Task DeleteOpeningAsync(int openingId);

        event Action IdRangeChanged;
    }
}