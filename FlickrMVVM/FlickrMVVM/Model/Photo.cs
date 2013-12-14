using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrMVVM.Model
{
    public class Photo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string SmallUrl { get; set; }
        public string LargeUrl { get; set; }

        public Photo() { }

        public Photo(int id, string title, string smallUrl, string largeUrl)
        {
            ID = id;
            Title = title;
            SmallUrl = smallUrl;
            LargeUrl = largeUrl;
        }
    }
}