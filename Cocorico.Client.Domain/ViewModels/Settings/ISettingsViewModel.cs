using Cocorico.Shared.Dtos;
using Cocorico.Shared.Dtos.Opening;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Settings
{
    public interface ISettingsViewModel : INotifyPropertyChanged
    {
        MutableRange IdRange { get; set; }
        ICollection<OpeningDto> Openings { get; }
        AddOpeningDto OpeningToAdd { get; set; }

        Task InitializeAsync();
        Task SetNewRangeAsync();
        Task AddOpeningAsync();
        Task EditOpeningAsync(OpeningDto opening);
        Task DeleteOpeningAsync(int openingId);
    }
}