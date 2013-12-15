using FlickrMVVM.Model;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;

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

        private object GoToPage2()
        {
            var msg = new PageMessage() { PageType = typeof(ImagePage) };

            Photo temp = new Photo(0, "test", "http://cs4653.vk.me/u18828099/video/l_f9e2ac9e.jpg", "http://cs4653.vk.me/u18828099/video/l_f9e2ac9e.jpg"); 
            PageMessage.MessageParameters = temp;

            Messenger.Default.Send<PageMessage>(msg);
            return null;
        }
    }
}