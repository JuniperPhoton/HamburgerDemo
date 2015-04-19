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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace HamburgerDemo
{

    public sealed partial class MainPage : Page
    {
        bool _isInSide = false;
        private TranslateTransform transfer = new TranslateTransform();
        double _oriXPosition = 0;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

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
