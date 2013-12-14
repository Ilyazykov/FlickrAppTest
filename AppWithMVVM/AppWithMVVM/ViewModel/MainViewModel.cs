using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Popups;
using System;

namespace AppWithMVVM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _Title;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        private string _SmallUrl;
        public string SmallUrl
        {
            get
            {
                return _SmallUrl;
            }
            set
            {
                _SmallUrl = value;
            }
        }

        private string _LargeUrl;
        public string LargeUrl
        {
            get
            {
                return _LargeUrl;
            }
            set
            {
                _LargeUrl = value;
            }
        }

        public MainViewModel()
        {
            Title = "ololo";

            ViewPageCommand = new RelayCommand( () => GoToViewPage() );
        }

        private object GoToViewPage()
        {
            messageBox("Navigate to Page 2!");

            return null;
        }

        protected async void messageBox(string msg)
        {
            var msgDlg = new Windows.UI.Popups.MessageDialog(msg);
            msgDlg.DefaultCommandIndex = 1;
            await msgDlg.ShowAsync();
        }

        public RelayCommand ViewPageCommand
        {
            get;
            private set;
        }
    }
}