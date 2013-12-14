using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using AppWithMVVM.Model;
using System.Collections.Generic;

namespace AppWithMVVM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public List<Customer> Customers
        {
            get
            {
                var customerCollection = new CustomerCollection();
                return customerCollection.Customers;
            }
        }

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

        public RelayCommand NavigateToViewPage { get; private set; }

        public MainViewModel()
        {
            Title = "ololo";

            NavigateToViewPage = new RelayCommand( () => GoToViewPage() );
        }

        private object GoToViewPage()
        {
            var msg = new GoToPageMessage() { PageType = typeof(Views.ViewPage) };
            Messenger.Default.Send<GoToPageMessage>(msg);

            return null;
        }

    }
}