using DysproseTwo.Dialogs;
using DysproseTwo.Services;
using DysproseTwo.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DysproseTwo.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {
        MainViewModel _viewModel = new MainViewModel();
        public MainView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _viewModel.FadeTimerService.FadeCompleted += FadeTimerService_FadeCompleted;
        }

        private void FadeTimerService_FadeCompleted(object sender, Microsoft.Toolkit.Uwp.UI.Animations.AnimationSetCompletedEventArgs e)
        {
            FadeTextBox.Text = "";
        }

        private async void StopAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.StopAsync();
        }

        private async void PausePlayAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            switch (_viewModel.CurrentSessionState)
            {
                case Enums.DysproseSessionState.InProgress:
                    await _viewModel.PauseAsync();
                    break;
                case Enums.DysproseSessionState.Paused:
                    await _viewModel.ResumeAsync();
                    break;
                case Enums.DysproseSessionState.Stopped:
                    await _viewModel.StartAsync(FadeTextBox);
                    break;
            }
        }

        private async void FadeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_viewModel.CurrentSessionState == Enums.DysproseSessionState.InProgress)
            {
                if (FadeTextBox.Text.Length > 0)
                {
                    await _viewModel.FadeTimerService.StartAsync();
                }
                else
                {
                    await _viewModel.FadeTimerService.StopAsync();
                } 
            }
        }

        private async void ShowDialogButton_Click(object sender, RoutedEventArgs e)
        {
            string sessionText = FadeTextBox.Text;
            try
            {
                var sessionCompletedDialog = new SessionCompletedDialog(sessionText);
                await sessionCompletedDialog.ShowAsync();

            }
            catch (Exception ex)
            {

            }
        }


        private void FlyoutShareClick(object sender, RoutedEventArgs e)
        {
            string sessionText = FadeTextBox.Text;
            ShareService.Instance.ShareSessionText(sessionText);
        }

        private void FlyoutCopyClick(object sender, RoutedEventArgs e)
        {
            string sessionText = FadeTextBox.Text;
            ShareService.CopySessionText(sessionText);
        }
    }
}
