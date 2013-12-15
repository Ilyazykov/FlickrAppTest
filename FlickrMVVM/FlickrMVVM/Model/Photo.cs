using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace FlickrMVVM.Model
{
    public class Photo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string SmallUrl { get; set; }
        public string LargeUrl { get; set; }

        public RelayCommand Page2Command { get; private set; }

        public Photo() { }

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
            var goToPage = new PageMessage() { PageType = typeof(ImagePage) };

            PageMessage.MessageParameters = this;

            Messenger.Default.Send<PageMessage>(goToPage);
            return null;
        }
    }
}