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
        private DysproseSessionSettings settings;
        private string textData;
        private SessionTimer timer;
        private DysproseSessionState state;
        

        public DysproseSessionSettings Settings { get => settings; set => settings = value; }
        public string TextData { get => textData; set => textData = value; }
        public SessionTimer Timer { get => timer; set => timer = value; }
        public DysproseSessionState State { get => state; set => state = value; }


        public Session()
        {
            Settings = new DysproseSessionSettings { FontSize = 15, SessionLength = new TimeSpan(0, 1, 0), FadeInterval = 5 };
            State = DysproseSessionState.Stopped;
            Timer = new SessionTimer(Settings.SessionLength);
        }

        public bool StartSession()
        {
            bool hasStarted = Timer.StartTimer();
            if (hasStarted)
            {
                State = DysproseSessionState.InProgress;
            }
            return hasStarted;
        }

        public void PauseSession()
        {
            Timer.PauseTimer();
            State = DysproseSessionState.Paused;
        }
    }
}
