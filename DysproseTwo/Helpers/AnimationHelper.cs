﻿using Microsoft.Toolkit.Uwp.UI.Animations;
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
        // Duration in milliseconds
        const float defaultAnimationDuration = 500;
        const double baseWidth = 400;
        static readonly List<AnimationSet> _runningFrameAnimations = new List<AnimationSet>();

        public static event EventHandler FrameSlideOutAnimationCompleted;
        
        public static async Task FrameSlideInAnimation(Frame frame)
        {
            StopAndClearRunningFrameAnimations();
            float windowWidth = GetWindowWidth();
            float animDuration = CalculateAnimationDuration(windowWidth);

            await frame.Offset(-windowWidth, 0, 0).StartAsync();
            frame.Visibility = Visibility.Visible;
            
            
            var slideInAnim = frame.Offset(0, duration:animDuration);
            slideInAnim.Completed += SlideInAnim_Completed;
            _runningFrameAnimations.Add(slideInAnim);
            await slideInAnim.StartAsync();
        }

        private static float CalculateAnimationDuration(float windowWidth)
        {
            
            return defaultAnimationDuration * SlideAnimationFunction(windowWidth/baseWidth);
        }

        private static float SlideAnimationFunction(double scale)
        {
            // y = mx + c

            float m = 0.25f;
            float x = (float)scale;
            float c = 0.75f;

            float y = m * x + c;

            return y;
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
            float animDuration = CalculateAnimationDuration(windowWidth);
            var slideOutAnim = frame.Offset(-windowWidth, duration:animDuration);
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
