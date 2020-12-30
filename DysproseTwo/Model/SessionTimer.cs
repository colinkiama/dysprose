using DysproseTwo.Structs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DysproseTwo.Model
{
    public class SessionTimer
    {
        public event EventHandler<TimeSpan> TimerTicked;
        public event EventHandler TimerEnded;

        readonly DispatcherTimer timer;
        long ticksPassed = 0;
        readonly long ticksToPass = 0;
        DateTime lastTime = DateTime.MinValue;


        public SessionTimer(DysproseSessionLength sessionTime)
        {
            timer = new DispatcherTimer();
            ticksPassed = 0;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            ticksToPass = TimeSpan.FromMilliseconds(sessionTime.TimeInMilliseconds).Ticks;
        }

        private void Timer_Tick(object sender, object e)
        {
            DateTime currentTime = DateTime.UtcNow;
            TimeSpan timeElapsed = currentTime - lastTime;
            lastTime = DateTime.UtcNow;
            ticksPassed += timeElapsed.Ticks;
            Debug.WriteLine($"Ticks Passed: {ticksPassed}");
            if (ticksPassed > ticksToPass)
            {
                timer.Stop();
                TimerEnded?.Invoke(sender, EventArgs.Empty);
            }
            else
            {
                TimeSpan totalTimeElapsed = TimeSpan.FromTicks(ticksPassed);
                TimerTicked?.Invoke(this, totalTimeElapsed);
            }
            
        }

        public bool StartTimer()
        {
            bool willStart = ticksToPass > ticksPassed;
            if (willStart)
            {
                lastTime = DateTime.UtcNow;
                timer.Start();
            }

            return willStart;
        }

        public void StopTimer()
        {
            timer.Stop();
        }
    }
}
