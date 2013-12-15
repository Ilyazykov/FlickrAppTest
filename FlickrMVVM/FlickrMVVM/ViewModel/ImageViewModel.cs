using FlickrMVVM.Model;
using GalaSoft.MvvmLight;

namespace FlickrMVVM.ViewModel
{
    public class ImageViewModel : ViewModelBase
    {
        public Photo Model 
        {
            get
            {
                return PageMessage.MessageParameters;
            }
        }

        public ImageViewModel() { }
    }
}