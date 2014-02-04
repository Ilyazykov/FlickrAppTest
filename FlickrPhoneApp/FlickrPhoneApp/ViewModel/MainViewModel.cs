using FlickrPhoneApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace FlickrPhoneApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Photo> Photos
        {
            get
            {
                var photoCollection = new MyPhotoCollection();
                return photoCollection.Photos;
            }
        }

        public string ApplicationTitle { get; set; }
        public string PageName { get; set; }

        public MainViewModel()
        {
            ApplicationTitle = "Flickr";
            PageName = "Photos";

            DetailsPageCommand = new RelayCommand<Photo>( ( msg ) => GoToDetailsPage( msg ) );
            Page2Command = new RelayCommand( () => GoToPage2() );
        }

        private object GoToDetailsPage(Photo msg)
        {
            System.Windows.MessageBox.Show("Go to details page with: " + msg.Title);
            return null;
        }

        private object GoToPage2()
        {
            var msg = new GoToPageMessage() { PageName = "ImagePage" };
            Messenger.Default.Send<GoToPageMessage>(msg);
            return null;
        }

        public RelayCommand<Photo> DetailsPageCommand
        {
            get;
            private set;
        }

        public RelayCommand Page2Command
        {
            get;
            private set;
        }
    }
}