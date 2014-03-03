using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;

namespace FlickrPhoneApp
{
    public partial class ImagePage : PhoneApplicationPage
    {
        public ImagePage()
        {
            InitializeComponent();
        }

        private void Image_Tap_1(object sender, GestureEventArgs e)
        {
            if (PageTitle.Opacity == 1)
            {
                TitleStoryboard.Begin();
            }
            else
            {
                TitleStoryboardReverse.Begin();
            }
        }
    }
}