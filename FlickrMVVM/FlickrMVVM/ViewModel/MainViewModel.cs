using FlickrMVVM.Model;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using Windows.UI.Xaml.Controls;

namespace FlickrMVVM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Photo> Photos { get; set; }

        public string Title { get; set; }

        public MainViewModel()
        {
            Title = "Flickr";

            var photoCollection = new MyPhotoCollection();
            Photos = photoCollection.Photos;
        }
    }
}