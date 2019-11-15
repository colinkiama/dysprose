using DysproseTwo.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DysproseTwo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MainPageViewModel ViewModel = new MainPageViewModel();
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.ObtainFadeElement(FadeTextBox);
            ViewModel.FadeTimerService.FadeCompleted += FadeTimerService_FadeCompleted;
        }

        private void FadeTimerService_FadeCompleted(object sender, Microsoft.Toolkit.Uwp.UI.Animations.AnimationSetCompletedEventArgs e)
        {
            FadeTextBox.Text = "";
        }

        private async void TimerButton_Click(object sender, RoutedEventArgs e)
        {
            switch (ViewModel.CurrentSession.State)
            {
                case Enums.DysproseSessionState.InProgress:
                    await ViewModel.PauseAsync().ConfigureAwait(true);
                    break;
                case Enums.DysproseSessionState.Paused:
                    await ViewModel.ResumeAsync().ConfigureAwait(true);
                    break;
                case Enums.DysproseSessionState.Stopped:
                    await ViewModel.StartAsync().ConfigureAwait(true);
                    break;
            }
        }

        private async void FadeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FadeTextBox.Text.Length > 0)
            {
                await ViewModel.FadeTimerService.StartAsync();
            }
            else
            {
                await ViewModel.FadeTimerService.StopAsync();
            }
        }
    }
}
