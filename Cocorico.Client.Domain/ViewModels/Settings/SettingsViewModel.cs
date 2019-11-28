using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using System;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Settings
{
    public class SettingsViewModel : ISettingsViewModel
    {
        private readonly ISettingsClient _settingsClient;

        public SettingsViewModel(ISettingsClient settingsClient) =>
            _settingsClient = settingsClient;

        public MutableRange IdRange { get; set; } = new MutableRange { End = 30, Start = 0 };

        public async Task InitializeAsync()
        {
            var currentRange = await _settingsClient.CurrentRangeAsync();

            IdRange.Start = currentRange.Start;
            IdRange.End = currentRange.End;

            IdRangeChanged?.Invoke();
        }

        public async Task SetNewRangeAsync()
        {
            try
            {
                var response = await _settingsClient.SetCurrentRangeAsync(IdRange);

                if (!response.IsSuccessfulStatusCode()) throw new UnexpectedException();
            }
            catch (SwaggerException)
            {
                // TODO: Handle Fail
            }
        }

        public event Action? IdRangeChanged;
    }
}
