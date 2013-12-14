using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using FlickrMVVM.Model;

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

        public MainViewModel()
        {
            Title = "Flickr";
        }
    }
}