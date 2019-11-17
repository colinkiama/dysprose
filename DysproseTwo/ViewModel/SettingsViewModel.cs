using DysproseTwo.Enums;
using DysproseTwo.Services;
using DysproseTwo.Structs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DysproseTwo.ViewModel
{
    class SettingsViewModel : Notifier
    {

        private int _sessionLength;

        public int SessionLength
        {
            get { return _sessionLength; }
            set
            {
                if (_sessionLength != value)
                {
                    _sessionLength = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private int _fadeIntervalValue;

        public int FadeIntervalValue
        {
            get { return _fadeIntervalValue; }
            set
            {
                if (_fadeIntervalValue != value)
                {
                    _fadeIntervalValue = value;
                    NotifyPropertyChanged();
                }

            }
        }


        private TimeUnit _selectedTimeUnit;

        public TimeUnit SelectedTimeUnit
        {
            get { return _selectedTimeUnit; }
            set
            {
                if (_selectedTimeUnit != value)
                {
                    _selectedTimeUnit = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private double _fontSize;

        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public List<double> FontList { get; set; } = new List<double>()
        {
            8,9,10,11,12,14,15,18,24,30,36,48,60,72,96
        };

        public List<TimeUnit> TimeUnits { get; set; } = new List<TimeUnit>()
        {
            TimeUnit.Seconds, TimeUnit.Minutes, TimeUnit.Hours
        };


        public SettingsViewModel()
        {
            MenuButtonService.Instance.MenuClosed += Instance_MenuClosed;
        }

        private void Instance_MenuClosed(object sender, EventArgs e)
        {
            SetSettings();
        }

        public void GetSettings()
        {
            var sessionSettings = SettingsService.Instance.SessionSettings;
            var globalSettings = SettingsService.Instance.GlobalSettings;

            AddSessionSettingsToViewModel(sessionSettings);
            AddGlobalSettingsToViewModel(globalSettings);
        }

        public void SetSettings()
        {
            var sessionSettings = GetSessionSettingsFromViewModel();
            var globalSettings = GetGlobalSettingsFromViewModel();

            SettingsService.Instance.UpdateSessionSettings(sessionSettings);
            SettingsService.Instance.UpdateGlobalSettings(globalSettings);
        }

        private DysproseGlobalSettings GetGlobalSettingsFromViewModel() => new DysproseGlobalSettings { FontSize = this.FontSize };

        private DysproseSessionSettings GetSessionSettingsFromViewModel() => new DysproseSessionSettings
        {
            FadeInterval = FadeIntervalValue,
            SessionLength = new DysproseSessionLength { Length = SessionLength, UnitOfLength = SelectedTimeUnit }
        };


        private void AddGlobalSettingsToViewModel(DysproseGlobalSettings globalSettings)
        {
            FontSize = globalSettings.FontSize;
        }

        private void AddSessionSettingsToViewModel(DysproseSessionSettings sessionSettings)
        {
            FadeIntervalValue = sessionSettings.FadeInterval;
            SessionLength = sessionSettings.SessionLength.Length;
            SelectedTimeUnit = sessionSettings.SessionLength.UnitOfLength;
        }
    }
}
