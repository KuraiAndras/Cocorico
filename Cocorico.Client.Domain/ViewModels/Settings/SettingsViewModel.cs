using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Opening;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Settings
{
    public class SettingsViewModel : ISettingsViewModel
    {
        private readonly ISettingsClient _settingsClient;

        public SettingsViewModel(ISettingsClient settingsClient) =>
            _settingsClient = settingsClient;

        public MutableRange IdRange { get; set; } = new MutableRange { End = 30, Start = 0 };
        public ICollection<OpeningDto> Openings { get; private set; } = new List<OpeningDto>();
        public AddOpeningDto OpeningToAdd { get; set; } = new AddOpeningDto();

        public async Task InitializeAsync()
        {
            var currentRangeTask = _settingsClient.CurrentRangeAsync();
            var openingsTask = _settingsClient.GetAllOpeningsAsync();

            var tasks = new Task[]
            {
                currentRangeTask,
                openingsTask,
            };

            await Task.WhenAll(tasks);

            IdRange.Start = currentRangeTask.Result.Start;
            IdRange.End = currentRangeTask.Result.End;

            Openings = openingsTask.Result;

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
                // TODO: Handle Fail
            }
        }

        public async Task AddOpeningAsync()
        {
            try
            {
                var response = await _settingsClient.AddOpeningAsync(OpeningToAdd);

                if (!response.IsSuccessfulStatusCode()) throw new UnexpectedException();

                OpeningToAdd = new AddOpeningDto();

                OnPropertyChanged(nameof(OpeningToAdd));

                await InitializeAsync();
            }
            catch (SwaggerException)
            {
                // TODO: Handle Fail
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
                // TODO: Handle Fail
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
                // TODO: Handle Fail
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
