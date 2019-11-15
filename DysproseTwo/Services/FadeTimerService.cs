using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DysproseTwo.Services
{
    public class FadeTimerService
    {
        public event EventHandler<AnimationSetCompletedEventArgs> FadeCompleted;
        List<AnimationSet> _animations = new List<AnimationSet>();
        // Seconds to milliseconds conversion (seconds * 1000)
        const float FadeAnimationTime = 5f * 1000;
        //const float ShowAnimationTime = 300f;

        FrameworkElement _controlToFade;


        public FadeTimerService(FrameworkElement controlToFade)
        {
            _controlToFade = controlToFade;
        }

        public async Task StartAsync()
        {
            int count = _animations.Count;
            if (count > 0)
            {
                List<AnimationSet> animationsToRemove = new List<AnimationSet>();
                for (int i = 0; i < count; i++)
                {
                    _animations[i].Completed -= Fade_Completed;
                    _animations[i].Stop();
                    animationsToRemove.Add(_animations[i]);
                }
                int deleteCount = animationsToRemove.Count;
                for (int i = 0; i < count; i++)
                {
                    _animations.Remove(animationsToRemove[i]);
                }
                animationsToRemove.Clear();
            }



            await _controlToFade.Fade(1, 0).StartAsync().ConfigureAwait(true);



            double duration = FadeAnimationTime;
            var fade = _controlToFade.Fade(0, duration / 2, duration / 2);
            fade.Completed += Fade_Completed;
            _animations.Add(fade);
            await fade.StartAsync().ConfigureAwait(false);

        }


        public async Task StopAsync()
        {
            int animationsCount = _animations.Count;
            for (int i = animationsCount - 1; i >= 0; i--)
            {
                CancelAndRemoveAnimation(_animations[i]);
            }

            await _controlToFade.Fade(1, 0).StartAsync().ConfigureAwait(true);
        }

        private void CancelAndRemoveAnimation(AnimationSet animationSet)
        {
            animationSet.Completed -= FadeCompleted;
            _animations.Remove(animationSet);
        }

        private void Fade_Completed(object sender, AnimationSetCompletedEventArgs e)
        {
            if (sender is AnimationSet animSet)
            {
                FadeCompleted?.Invoke(sender, e);
                CancelAndRemoveAnimation(animSet);
            }

        }
    }
}
