﻿using DysproseTwo.Structs;
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
            if (sessionSettings != _sessionSettings)
            {
                sessionSettings = _sessionSettings;
                SessionSettingsUpdated?.Invoke(this, sessionSettings);
            }
        }

        public void UpdateGlobalSettings(DysproseGlobalSettings globalSettings)
        {
            if (globalSettings != _globalSettings)
            {
                globalSettings = _globalSettings;
                GlobalSettingsUpdated?.Invoke(this, globalSettings);
            }
        }


    }
}