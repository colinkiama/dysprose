using DysproseTwo.Enums;
using DysproseTwo.Model;
using DysproseTwo.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DysproseTwo.ViewModel
{
    class MainViewModel : Notifier
    {

        private double _fontSize;

        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                NotifyPropertyChanged();
            }
        }


        private TimeSpan _sessionLength;

        private TimeSpan _currentSessionTime;

        private FadeTimerService _fadeTimerService;

        public TimeSpan CurrentSessionTime
        {
            get { return _currentSessionTime; }
            set
            {
                _currentSessionTime = value;
                NotifyPropertyChanged();
            }
        }

        private DysproseSessionState _currentSessionState;

        internal async Task StopAsync()
        {
            CurrentSession.StopSession();
            await FadeTimerService.StopAsync();
        }

        public DysproseSessionState CurrentSessionState
        {
            get { return _currentSessionState; }
            set
            {
                _currentSessionState = value;
                SessionService.Instance.UpdateSessionState(_currentSessionState);
                NotifyPropertyChanged();
            }
        }



        public FadeTimerService FadeTimerService { get => _fadeTimerService; private set => _fadeTimerService = value; }

        private Session _session;

        public Session CurrentSession
        {
            get { return _session; }
            set
            {
                _session = value;
                NotifyPropertyChanged();
            }
        }


        public MainViewModel()
        {
            CurrentSession = new Session();
            FillInDetailsFromSession();
            FontSize = SettingsService.Instance.GlobalSettings.FontSize;

            // Just for testing
            CurrentSessionState = DysproseSessionState.Stopped;
        }

        private void FillInDetailsFromSession()
        {
            CurrentSession.TimerTicked += CurrentSession_TimerTicked;
            CurrentSession.SessionCompleted += CurrentSession_SessionCompleted;
            CurrentSession.StateChanged += CurrentSession_StateChanged;

            _sessionLength = TimeSpan.FromMilliseconds(CurrentSession.Settings.SessionLength.TimeInMilliseconds);
            CurrentSessionTime = _sessionLength;
            SettingsService.Instance.GlobalSettingsUpdated += Instance_GlobalSettingsUpdated;
            SettingsService.Instance.SessionSettingsUpdated += Instance_SessionSettingsUpdated;
        }

        private void CurrentSession_SessionCompleted(object sender, EventArgs e)
        {
            CurrentSessionTime = new TimeSpan(0);
        }

        private void CurrentSession_StateChanged(object sender, DysproseSessionState newState)
        {
            CurrentSessionState = newState;
        }

        private void Instance_SessionSettingsUpdated(object sender, Structs.DysproseSessionSettings e)
        {
            CurrentSession = new Session(e);
            FillInDetailsFromSession();
        }

        private void Instance_GlobalSettingsUpdated(object sender, Structs.DysproseGlobalSettings globalSettings)
        {
            FontSize = globalSettings.FontSize;
        }

        internal async Task ResumeAsync()
        {
            CurrentSession.StartSession();
            await FadeTimerService.StartAsync().ConfigureAwait(true);
        }

        internal async Task StartAsync()
        {
            bool hasSessionStarted = CurrentSession.StartSession();
            if (hasSessionStarted)
            {
                await FadeTimerService.StartAsync().ConfigureAwait(true);
            }
        }

        internal async Task PauseAsync()
        {
            CurrentSession.PauseSession();
            await FadeTimerService.StopAsync().ConfigureAwait(true);
        }

        internal async Task ResetFadeAsync()
        {
            await FadeTimerService.StartAsync().ConfigureAwait(true);
        }

        public void ObtainFadeElement(FrameworkElement elementToFade)
        {
            FadeTimerService = new FadeTimerService(elementToFade, CurrentSession.Settings.FadeInterval);
        }
       

        private void CurrentSession_TimerTicked(object sender, TimeSpan timeElapsed)
        {
            CurrentSessionTime = _sessionLength - timeElapsed;
        }

        public void UpdateFadeTime(int updatedFadeInterval)
        {
            FadeTimerService.UpdateFadeInterval(updatedFadeInterval);
        }

    }
}
