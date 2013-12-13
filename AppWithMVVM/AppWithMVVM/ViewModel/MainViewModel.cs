using GalaSoft.MvvmLight;

namespace AppWithMVVM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _Title;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        private string _SmallUrl;
        public string SmallUrl
        {
            get
            {
                return _SmallUrl;
            }
            set
            {
                _SmallUrl = value;
            }
        }

        private string _LargeUrl;
        public string LargeUrl
        {
            get
            {
                return _LargeUrl;
            }
            set
            {
                _LargeUrl = value;
            }
        }

        public MainViewModel()
        {
            Title = "ololo";
        }
    }
}