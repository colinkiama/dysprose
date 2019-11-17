using DysproseTwo.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DysproseTwo.Services
{
    public class SettingsService
    {
        public DysproseSessionSettings SessionSettings { get => _sessionSettings; set => _sessionSettings = value; }
        public DysproseGlobalSettings GlobalSettings { get => _globalSettings; set => _globalSettings = value; }

        private DysproseSessionSettings _sessionSettings;

        private DysproseGlobalSettings _globalSettings;

        public event EventHandler<DysproseSessionSettings> SessionSettingsUpdated;
        public event EventHandler<DysproseGlobalSettings> GlobalSettingsUpdated;

        // Singleton Pattern with "Lazy"
        private SettingsService _settingsService = null;
        private static Lazy<SettingsService> lazy =
            new Lazy<SettingsService>(() => new SettingsService());

        public static SettingsService Instance => lazy.Value;

        private SettingsService() { }

        public void UpdateSessionSettings(DysproseSessionSettings sessionSettings)
        {
            if (sessionSettings != this._sessionSettings)
            {
                this._sessionSettings = sessionSettings;
                SessionSettingsUpdated?.Invoke(this, sessionSettings);
            }
        }

        public void UpdateGlobalSettings(DysproseGlobalSettings globalSettings)
        {
            if (globalSettings != this._globalSettings)
            {
                this._globalSettings = globalSettings;
                GlobalSettingsUpdated?.Invoke(this, globalSettings);
            }
        }

        public async Task LoadSettingsAsync()
        {
            // TODO: Load from file
            SessionSettings = new DysproseSessionSettings
            {
                FadeInterval = 5,
                SessionLength = new DysproseSessionLength
                {
                    Length = 1,
                    UnitOfLength = Enums.TimeUnit.Minutes
                }
            };

            GlobalSettings = new DysproseGlobalSettings { FontSize = 15 };
        }

        public async Task SaveSettingsAsync()
        {
            // TODO: Save to file
        }

    }
}
