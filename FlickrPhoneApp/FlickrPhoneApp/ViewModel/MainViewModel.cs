using GalaSoft.MvvmLight;

namespace FlickrPhoneApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public string ApplicationTitle { get; set; }
        public string PageName { get; set; }

        public MainViewModel()
        {
            ApplicationTitle = "Flickr";
            PageName = "Photos";
        }
    }
}