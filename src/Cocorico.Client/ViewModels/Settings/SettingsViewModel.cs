using Cocorico.Client.Extensions;
using Cocorico.Client.HttpClient;
using Cocorico.Shared.Api.Openings;
using Cocorico.Shared.Dtos;
using Cocorico.Shared.Exceptions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Settings
{
    public class SettingsViewModel : ISettingsViewModel
    {
        private readonly ISettingsClient _settingsClient;

        public SettingsViewModel(ISettingsClient settingsClient) =>
            _settingsClient = settingsClient;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MutableRange IdRange { get; set; } = new MutableRange { End = 30, Start = 0 };
        public ICollection<OpeningDto> Openings { get; private set; } = new List<OpeningDto>();
        public AddOpening OpeningToAdd { get; set; } = new AddOpening();

        public async Task InitializeAsync()
        {
            var currentRangeTask = await _settingsClient.CurrentRangeAsync();
            var openingsTask = await _settingsClient.GetAllOpeningsAsync();

            IdRange.Start = currentRangeTask.Start;
            IdRange.End = currentRangeTask.End;

            Openings = openingsTask;

            OnPropertyChanged(nameof(IdRange));
            OnPropertyChanged(nameof(Openings));
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
            }
        }

        public async Task AddOpeningAsync()
        {
            try
            {
                var response = await _settingsClient.AddOpeningAsync(OpeningToAdd);

                if (!response.IsSuccessfulStatusCode()) throw new UnexpectedException();

                OpeningToAdd = new AddOpening();

                OnPropertyChanged(nameof(OpeningToAdd));

                await InitializeAsync();
            }
            catch (SwaggerException)
            {
            }
        }

        public async Task EditOpeningAsync(OpeningDto opening)
        {
            try
            {
                var response = await _settingsClient.UpdateOpeningAsync(opening);

                if (!response.IsSuccessfulStatusCode()) throw new UnexpectedException();

                await InitializeAsync();
            }
            catch (SwaggerException)
            {
            }
        }

        public async Task DeleteOpeningAsync(int openingId)
        {
            try
            {
                var response = await _settingsClient.DeleteOpeningAsync(openingId);

                if (!response.IsSuccessfulStatusCode()) throw new UnexpectedException();

                await InitializeAsync();
            }
            catch (SwaggerException)
            {
            }
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
