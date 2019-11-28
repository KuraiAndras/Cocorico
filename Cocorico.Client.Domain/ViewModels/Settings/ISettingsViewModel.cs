using Cocorico.Shared.Helpers;
using System;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Settings
{
    public interface ISettingsViewModel
    {
        MutableRange IdRange { get; set; }

        Task InitializeAsync();
        Task SetNewRangeAsync();

        event Action IdRangeChanged;
    }
}