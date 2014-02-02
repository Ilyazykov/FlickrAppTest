using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace FlickrPhoneApp.Model
{
    public class Photo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string SmallUrl { get; set; }
        public string LargeUrl { get; set; }

        public RelayCommand Page2Command { get; private set; }

        public Photo(int id, string title, string smallUrl, string largeUrl)
        {
            ID = id;
            Title = title;
            SmallUrl = smallUrl;
            LargeUrl = largeUrl;

            Page2Command = new RelayCommand(() => GoToPage2());
        }

        private object GoToPage2()
        {
            //var dlg = new MessageDialog(Title);
            //dlg.ShowAsync()

            PageMessage.PageType = typeof(ImagePage);
            PageMessage.MessageParameters = this;

            PageMessage temp = new PageMessage();
            Messenger.Default.Send<PageMessage>(temp);

            return null;
        }
    }
}
