using Cocorico.Shared.Api;
using Cocorico.Shared.Api.Openings;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Settings
{
    public interface ISettingsViewModel : INotifyPropertyChanged
    {
        MutableRange IdRange { get; set; }
        ICollection<OpeningDto> Openings { get; }
        AddOpening OpeningToAdd { get; set; }

        Task InitializeAsync();
        Task SetNewRangeAsync();
        Task AddOpeningAsync();
        Task EditOpeningAsync(UpdateOpening opening);
        Task DeleteOpeningAsync(int openingId);
    }
}
