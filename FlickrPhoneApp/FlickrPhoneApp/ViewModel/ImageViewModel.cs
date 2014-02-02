using GalaSoft.MvvmLight;

namespace FlickrPhoneApp.ViewModel
{
    class ImageViewModel
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
