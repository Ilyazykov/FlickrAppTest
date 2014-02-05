using FlickrPhoneApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace FlickrPhoneApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Photo> Photos { get; set; }

        public string ApplicationTitle { get; set; }
        public string PageName { get; set; }


        public MainViewModel()
        {
            ApplicationTitle = "Flickr";
            PageName = "Photos";

            var photoCollection = new MyPhotoCollection();
            Photos = photoCollection.Photos;

            DetailsPageCommand = new RelayCommand<Photo>( ( msg ) => GoToDetailsPage( msg ) );
        }



        private object GoToDetailsPage(Photo selectedPhoto)
        {
            var msg = new GoToPageMessage("ImagePage", selectedPhoto);
            Messenger.Default.Send<GoToPageMessage>(msg);
            return null;
        }

        public RelayCommand<Photo> DetailsPageCommand
        {
            get;
            private set;
        }
    }
}