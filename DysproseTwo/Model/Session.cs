using DysproseTwo.Enums;
using DysproseTwo.Services;
using DysproseTwo.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DysproseTwo.Model
{
    public class Session
    {
        private DysproseSessionSettings settings;
        private string textData;
        private SessionTimer timer;
        private DysproseSessionState state;
        private DysproseSessionSettings e;

        public DysproseSessionSettings Settings { get => settings; set => settings = value; }
        public string TextData { get => textData; set => textData = value; }
        public SessionTimer Timer { get => timer; set => timer = value; }
        public DysproseSessionState State { get => state; set => state = value; }

        public event EventHandler<DysproseSessionLength> SessionLengthUpdated;
        public event EventHandler<int> FadeIntervalUpdated;
        public event EventHandler<TimeSpan> TimerTicked;
        public event EventHandler SessionCompleted;
        public event EventHandler<DysproseSessionState> StateChanged;

        public Session()
        {
            var loadedSessionSettings = SettingsService.Instance.SessionSettings;
            Settings = new DysproseSessionSettings {SessionLength = loadedSessionSettings.SessionLength , FadeInterval = loadedSessionSettings.FadeInterval};
            State = DysproseSessionState.Stopped;
            Timer = new SessionTimer(settings.SessionLength);
            Timer.TimerTicked += Timer_TimerTicked;
            Timer.TimerEnded += Timer_TimerEnded;
        }

        private void Timer_TimerEnded(object sender, EventArgs e)
        {
            SessionCompleted?.Invoke(sender, e);
        }

        private void Timer_TimerTicked(object sender, TimeSpan e)
        {
            TimerTicked?.Invoke(sender, e);
        }

        public Session(DysproseSessionSettings sessionSettings)
        {
            Settings = sessionSettings;
            Timer = new SessionTimer(settings.SessionLength);
            State = DysproseSessionState.Stopped;
        }

        public bool StartSession()
        {
            Timer = new SessionTimer(settings.SessionLength);
            bool hasStarted = Timer.StartTimer();
            if (hasStarted)
            {
                State = DysproseSessionState.InProgress;
                StateChanged?.Invoke(this, State);
            }
            return hasStarted;
        }

        public void UpdateSessionLength(DysproseSessionLength updatedSessionLength)
        {
            var oldSettings = Settings;
            Settings = new DysproseSessionSettings { FadeInterval = 5, SessionLength = updatedSessionLength };
            SessionLengthUpdated?.Invoke(this, updatedSessionLength);
        }

        public void UpdateFadeInterval(int updatedFadeInterval)
        {
            var oldSettings = Settings;
            Settings = new DysproseSessionSettings { FadeInterval = updatedFadeInterval, SessionLength = oldSettings.SessionLength };
            FadeIntervalUpdated?.Invoke(this, updatedFadeInterval);
        }

        public void PauseSession()
        {
            Timer.StopTimer();
            State = DysproseSessionState.Paused;
            StateChanged?.Invoke(this, State);
        }

        public void StopSession()
        {
            Timer.StopTimer();
            State = DysproseSessionState.Stopped;
            StateChanged?.Invoke(this, State);
        }

        public bool ResumeSession()
        {
            bool hasStarted = Timer.StartTimer();
            if (hasStarted)
            {
                State = DysproseSessionState.InProgress;
                StateChanged?.Invoke(this, State);
            }
            return hasStarted;
        }

    }
}
