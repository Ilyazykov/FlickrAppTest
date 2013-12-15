using FlickrMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using GalaSoft.MvvmLight.Messaging;

namespace FlickrMVVM
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Messenger.Default.Register<PageMessage>(this, (action) => ReceiveMessage(action));
        }

        private object ReceiveMessage(PageMessage action)
        {
            this.Frame.Navigate(action.PageType);
            return null;
        }

        private void q(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ImagePage));
        }
    }
}