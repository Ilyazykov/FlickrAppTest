using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using FlickrMVVM.Model;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Popups;

namespace FlickrMVVM.ViewModel
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

        public string Title { get; set; }

        public RelayCommand Page2Command { get; private set; }

        public MainViewModel()
        {
            Title = "Flickr";

            Page2Command = new RelayCommand( () => GoToPage2() );
        }

        private void GoToPage2()
        {
            var dlg = new MessageDialog("Navigate to Page 2!");
            dlg.ShowAsync();
        }
    }
}