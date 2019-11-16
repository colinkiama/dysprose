using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DysproseTwo.Helpers
{
    public static class AnimationHelper
    {
        static List<AnimationSet> _runningFrameAnimations = new List<AnimationSet>();

        public static event EventHandler FrameSlideOutAnimationCompleted;
        
        public static async Task FrameSlideInAnimation(Frame frame)
        {
            StopAndClearRunningFrameAnimations();
            float windowWidth = GetWindowWidth();
            await frame.Offset(-windowWidth, 0, 0).StartAsync();
            frame.Visibility = Visibility.Visible;
            
            
            var slideInAnim = frame.Offset(0);
            slideInAnim.Completed += SlideInAnim_Completed;
            _runningFrameAnimations.Add(slideInAnim);
            await slideInAnim.StartAsync();
        }

        private static void SlideInAnim_Completed(object sender, AnimationSetCompletedEventArgs e)
        {
            _runningFrameAnimations.Clear();
        }

        private static void StopAndClearRunningFrameAnimations()
        {
            foreach (var anim in _runningFrameAnimations)
            {
                anim.Dispose();
            }
            _runningFrameAnimations.Clear();
        }

        public static async Task FrameSlideOutAnimation(Frame frame)
        {
            StopAndClearRunningFrameAnimations();
            float windowWidth = GetWindowWidth();
            var slideOutAnim = frame.Offset(-windowWidth);
            slideOutAnim.Completed += SlideOutAnim_Completed;
            _runningFrameAnimations.Add(slideOutAnim);
            await slideOutAnim.StartAsync();
        }

        private static void SlideOutAnim_Completed(object sender, AnimationSetCompletedEventArgs e)
        {
            _runningFrameAnimations.Clear();
            FrameSlideOutAnimationCompleted?.Invoke(null, EventArgs.Empty);
        }

        private static float GetWindowWidth()
        {
            return (float)Window.Current.Bounds.Width;
        }
    }
}
