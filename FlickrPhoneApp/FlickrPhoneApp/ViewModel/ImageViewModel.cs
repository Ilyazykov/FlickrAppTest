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
                return "Welcome to Page 2";
            }
        }

        public ImageViewModel()
        {
        }
    }
}
