using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DysproseTwo.Model
{
    public class SessionTimer
    {
        DispatcherTimer timer;
        int timesTicked = 0;
        int timesToTick = 0;


        public SessionTimer(TimeSpan sessionTime)
        {
            timer = new DispatcherTimer();
            timesTicked = 0;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            timesToTick = (int)Math.Ceiling(sessionTime / timer.Interval);
        }

        private void Timer_Tick(object sender, object e)
        {
            timesToTick++;
        }

        public bool StartTimer()
        {
            bool willStart = timesTicked > timesToTick;
            if (willStart)
            {
                timer.Start();
            }

            return willStart;
        }

        public void PauseTimer()
        {
            timer.Stop();
        }
    }
}
