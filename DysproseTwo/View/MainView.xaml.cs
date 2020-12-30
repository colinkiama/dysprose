using DysproseTwo.Enums;
using DysproseTwo.Services;
using DysproseTwo.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DysproseTwo.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {
        readonly MainViewModel _viewModel = new MainViewModel();
        public MainView()
        {
            this.InitializeComponent();

            FadeTextBox.PreviewKeyDown += HandleFadeTextBoxKeyPress;
            FadeTextBox.PreviewKeyUp += HandleFadeTextBoxKeyPress;
        }


        private void HandleFadeTextBoxKeyPress(object sender, KeyRoutedEventArgs e)
        {
            if (_viewModel.AreBackEditsDisabled)
            {
                if (e.Key == Windows.System.VirtualKey.Back
                               || e.Key == Windows.System.VirtualKey.Delete
                               || e.Key == Windows.System.VirtualKey.Left
                               || e.Key == Windows.System.VirtualKey.Right
                               || e.Key == Windows.System.VirtualKey.Up
                               || e.Key == Windows.System.VirtualKey.Down
                               || e.Key == Windows.System.VirtualKey.Home
                               || e.Key == Windows.System.VirtualKey.End
                               || e.Key == Windows.System.VirtualKey.PageUp
                               || e.Key == Windows.System.VirtualKey.PageDown)
                {

                    e.Handled = true;

                }
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }



        private async void StopAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.StopAsync();
        }

        private async void PausePlayAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            switch (_viewModel.CurrentSessionState)
            {
                case DysproseSessionState.InProgress:
                    await _viewModel.PauseAsync();
                    break;
                case DysproseSessionState.Paused:
                    await _viewModel.ResumeAsync();
                    break;
                case DysproseSessionState.Stopped:
                    await _viewModel.StartAsync(FadeTextBox);
                    break;
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

        private async void FadeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (_viewModel.CurrentSessionState == DysproseSessionState.InProgress)
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

        private void FadeTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CurrentSessionState != DysproseSessionState.Stopped)
            {
                FadeTextBox.SelectionLength = 0;

                if (_viewModel.AreBackEditsDisabled)
                {
                    FadeTextBox.SelectionStart = FadeTextBox.Text.Length;
                }
            }
        }


    }
}
