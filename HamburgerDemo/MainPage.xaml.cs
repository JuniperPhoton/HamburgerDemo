using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace HamburgerDemo
{

    public sealed partial class MainPage : Page
    {
        bool _isInSide = false;
        TranslateTransform transfer = new TranslateTransform();
        double _oriXPosition = 0;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            StatusBar.GetForCurrentView().BackgroundOpacity = 0.2;

            ContentGrid.ManipulationDelta += ContentGrid_ManipulationDelta;
            ContentGrid.ManipulationStarted += ContentGrid_ManipulationStarted;
            transfer = new TranslateTransform();
            ContentGrid.RenderTransform = this.transfer;
        }

        private void ContentGrid_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            _oriXPosition = e.Position.X;
        }

        private void ContentGrid_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (e.Delta.Translation.X > 10 && _oriXPosition <= 10)
            {
                if (!_isInSide)
                {
                    SideInStory.Begin();
                    HamInStory.Begin();
                    _isInSide = true;
                }
            }
            else if (e.Delta.Translation.X < -10)
            {
                if (_isInSide)
                {
                    SideOutStory.Begin();
                    HamOutStory.Begin();
                    _isInSide = false;
                }
            }
        }

        private void HamburgerClick(object sender, RoutedEventArgs e)
        {
            if (_isInSide)
            {
                SideOutStory.Begin();
                HamOutStory.Begin();
                _isInSide = false;
            }
            else
            {
                SideInStory.Begin();
                HamInStory.Begin();
                _isInSide = true;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            Frame.BackStack.Clear();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            if (_isInSide)
            {
                SideOutStory.Begin();
                HamOutStory.Begin();
                _isInSide = false;
                return;
            }
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (_isInSide)
            {
                e.Handled = true;
                SideOutStory.Begin();
                HamOutStory.Begin();
                _isInSide = false;
                return;
            }
            Frame rootframe = Window.Current.Content as Frame;
            if (rootframe != null && rootframe.CanGoBack)
            {
                e.Handled = true;
                rootframe.GoBack();
            }
        }

        

        private void TapTranBorder(object sender, RoutedEventArgs e)
        {
            if (_isInSide)
            {
                SideOutStory.Begin();
                HamOutStory.Begin();
                _isInSide = false;
                return;
            }
        }
    }
}
