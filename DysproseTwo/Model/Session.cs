using DysproseTwo.Enums;
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
        public SessionSettings Settings;
        public string TextData;
        public SessionTimer Timer;
        public SessionState State;

        public Session()
        {
            Settings = new SessionSettings { FontSize = 15, SessionLength = new TimeSpan(0, 1, 0) };
            State = SessionState.Stopped;
            Timer = new SessionTimer(Settings.SessionLength);
        }

        public bool StartSession()
        {
            bool hasStarted = Timer.StartTimer();
            if (hasStarted)
            {
                State = SessionState.InProgress;
            }
            return hasStarted;
        }

        public void PauseSession()
        {
            Timer.PauseTimer();
            State = SessionState.Paused;
        }
    }
}
