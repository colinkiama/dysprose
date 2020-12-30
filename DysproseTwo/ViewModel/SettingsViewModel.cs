﻿using DysproseTwo.Enums;
using DysproseTwo.Services;
using DysproseTwo.Structs;
using System;
using System.Collections.Generic;

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

        private bool _areBackEditsDisabled;

        public bool AreBackEditsDisabled
        {
            get { return _areBackEditsDisabled; }
            set
            {
                if (_areBackEditsDisabled != value)
                {
                    _areBackEditsDisabled = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // Beware of double negative!
        // This was done for easier xaml binding
        private bool _isSessionNotInProgress;

        public bool IsSessionNotInProgress
        {
            get { return _isSessionNotInProgress; }
            set
            {
                _isSessionNotInProgress = value;
                NotifyPropertyChanged();
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
            SessionService.Instance.SessionStateChanged += Instance_SessionStateChanged;
            GetSettings();
        }

        private void Instance_SessionStateChanged(object sender, DysproseSessionState sessionState)
        {
            switch (sessionState)
            {

                case DysproseSessionState.Stopped:
                    IsSessionNotInProgress = true;
                    break;
                default:
                    IsSessionNotInProgress = false;
                    break;
            }
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
            var globalSettings = GetGlobalSettingsFromViewModel();
            SettingsService.Instance.UpdateGlobalSettings(globalSettings);

            if (_isSessionNotInProgress)
            {
                var sessionSettings = GetSessionSettingsFromViewModel();
                SettingsService.Instance.UpdateSessionSettings(sessionSettings);
            }
        }

        private DysproseGlobalSettings GetGlobalSettingsFromViewModel() => new DysproseGlobalSettings
        {
            FontSize = this.FontSize,
        };

        private DysproseSessionSettings GetSessionSettingsFromViewModel() => new DysproseSessionSettings
        {
            FadeInterval = FadeIntervalValue,
            SessionLength = new DysproseSessionLength { Length = _sessionLength, UnitOfLength = _selectedTimeUnit }, 
            AreBackEditsDisabled = this.AreBackEditsDisabled
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
            AreBackEditsDisabled = sessionSettings.AreBackEditsDisabled;
        }

    }
}
