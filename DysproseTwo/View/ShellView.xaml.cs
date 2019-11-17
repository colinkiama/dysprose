using DysproseTwo.Helpers;
using DysproseTwo.Services;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DysproseTwo.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellView : Page
    {
        bool _isMenuOpen = false;

        public ShellView()
        {
            this.InitializeComponent();
            AnimationHelper.FrameSlideOutAnimationCompleted += AnimationHelper_FrameSlideOutAnimationCompleted;
        }

        private void AnimationHelper_FrameSlideOutAnimationCompleted(object sender, EventArgs e)
        {
            SettingsFrame.Visibility = Visibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MainFrame.Navigate(typeof(MainView));
            SettingsFrame.Navigate(typeof(SettingsView));
            SettingsFrame.Visibility = Visibility.Collapsed;
        }

        private async void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isMenuOpen)
            {
                MenuButtonService.Instance.CloseMenu();
                await AnimationHelper.FrameSlideOutAnimation(SettingsFrame);

            }
            else
            {
                await AnimationHelper.FrameSlideInAnimation(SettingsFrame);
            }
            _isMenuOpen = !_isMenuOpen;
        }
    }
}
