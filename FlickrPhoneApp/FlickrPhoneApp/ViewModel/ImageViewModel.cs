using FlickrPhoneApp.Model;
using GalaSoft.MvvmLight;

namespace FlickrPhoneApp.ViewModel
{
    public class ImageViewModel : ViewModelBase
    {
        public string ApplicationTitle 
        { 
            get
            {
                return "Flickr";
            }
        }

        public string PageName 
        { 
            get
            {
                return "Image";
            }
        }

        public string Welcome
        {
            get
            {
                return "Page2";
            }
        }

        public Photo SelectedPhoto
        {
            get
            {
                return GoToPageMessage.SelectedPhoto;
            }
        }

        public ImageViewModel() { }
    }
}
