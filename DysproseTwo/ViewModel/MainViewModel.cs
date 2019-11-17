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
        }

        private void FillInDetailsFromSession()
        {
            CurrentSession.Timer.TimerTicked += Timer_TimerTicked;
            CurrentSession.Timer.TimerEnded += Timer_TimerEnded;
            _sessionLength = TimeSpan.FromMilliseconds(CurrentSession.Settings.SessionLength.TimeInMilliseconds);
            CurrentSessionTime = _sessionLength;
            SettingsService.Instance.GlobalSettingsUpdated += Instance_GlobalSettingsUpdated;
            SettingsService.Instance.SessionSettingsUpdated += Instance_SessionSettingsUpdated;
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
            CurrentSession.StartSession();
            await FadeTimerService.StartAsync().ConfigureAwait(true);
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
        private void Timer_TimerEnded(object sender, EventArgs e)
        {
            CurrentSessionTime = new TimeSpan(0);
        }

        private void Timer_TimerTicked(object sender, TimeSpan timeElapsed)
        {
            CurrentSessionTime = _sessionLength - timeElapsed;
        }

        public void UpdateFadeTime(int updatedFadeInterval)
        {
            FadeTimerService.UpdateFadeInterval(updatedFadeInterval);
        }

    }
}
