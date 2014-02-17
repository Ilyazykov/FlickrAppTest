using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlickrPhoneApp.Model
{
    public class Photo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string SmallUrl { get; set; }
        public string LargeUrl { get; set; }

        public Photo(int id, string title, string smallUrl, string largeUrl)
        {
            ID = id;
            Title = title;
            SmallUrl = smallUrl;
            LargeUrl = largeUrl;
        }
    }
}
