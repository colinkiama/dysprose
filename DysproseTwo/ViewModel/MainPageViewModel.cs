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
    class MainPageViewModel : INotifyPropertyChanged
    {
        private TimeSpan _sessionLength;

        private TimeSpan _currentSessionTime;

        private FadeTimerService _fadeTimerService;
        public TimeSpan CurrentSessionTime
        {
            get { return _currentSessionTime; }
            set
            {
                _currentSessionTime = value;
                OnPropertyChanged();
            }
        }


        private Session _session;

        public Session CurrentSession
        {
            get { return _session; }
            set
            {
                _session = value;
                OnPropertyChanged();
            }
        }

        public FadeTimerService FadeTimerService { get => _fadeTimerService; set => _fadeTimerService = value; }

        public MainPageViewModel()
        {
            CurrentSession = new Session();
            _sessionLength = CurrentSession.Settings.SessionLength;
            CurrentSessionTime = _sessionLength;
            CurrentSession.Timer.TimerTicked += Timer_TimerTicked;
            CurrentSession.Timer.TimerEnded += Timer_TimerEnded;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
