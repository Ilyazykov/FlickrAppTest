using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using System.Text;

namespace FlickrPhoneApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Messenger.Default.Register<GoToPageMessage>
            (
                this,
                ( action ) => ReceivveMessage( action )
            );
        }

        private object ReceivveMessage(GoToPageMessage action)
        {
            StringBuilder sb = new StringBuilder("/");
            sb.Append(GoToPageMessage.PageName);
            sb.Append(".xaml");

            NavigationService.Navigate(new System.Uri(sb.ToString(), System.UriKind.Relative));
            return null;
        }
    }
}