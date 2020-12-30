using DysproseTwo.Dialogs;
using DysproseTwo.ViewModel;
using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DysproseTwo.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsView : Page
    {
        readonly SettingsViewModel _viewModel = new SettingsViewModel();
        readonly AboutDialog _aboutDialog = new AboutDialog();
        public SettingsView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private async void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _aboutDialog.ShowAsync();
            }
            catch (Exception)
            {

            }
        }

        private void FullScreenModeButton_Click(object sender, RoutedEventArgs e)
        {
            var appView = ApplicationView.GetForCurrentView();
            if (appView.IsFullScreenMode)
            {
                appView.ExitFullScreenMode();
            }
            else
            {
                appView.TryEnterFullScreenMode();
            }
        }
    }
}
