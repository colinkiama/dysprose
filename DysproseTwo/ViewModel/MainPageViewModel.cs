using DysproseTwo.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DysproseTwo.ViewModel
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private TimeSpan _sessionLength;

        private TimeSpan _currentSessionTime;

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

        public MainPageViewModel()
        {
            CurrentSession = new Session();
            _sessionLength = CurrentSession.Settings.SessionLength;
            CurrentSessionTime = _sessionLength;
            CurrentSession.Timer.TimerTicked += Timer_TimerTicked;
            CurrentSession.Timer.TimerEnded += Timer_TimerEnded;
        }

        private void Timer_TimerEnded(object sender, EventArgs e)
        {
            CurrentSessionTime = new TimeSpan(0);
        }

        private void Timer_TimerTicked(object sender, TimeSpan e)
        {
            // e = Time elapsed
            CurrentSessionTime = _sessionLength - e;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
