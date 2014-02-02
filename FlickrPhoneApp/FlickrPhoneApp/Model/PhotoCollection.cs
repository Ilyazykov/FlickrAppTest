using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

using FlickrNet;

namespace FlickrPhoneApp.Model
{
    class MyPhotoCollection
    {
        private readonly ObservableCollection<Photo> _photos = new ObservableCollection<Photo>();
        public ObservableCollection<Photo> Photos
        {
            get { return _photos; }
        }

        public Photo GetPhotoByID(int id)
        {
            return _photos[id];
        }

        public MyPhotoCollection()
        {
            GetPhotosFromFlickr(12);
        }

        private void GetPhotosFromFlickr(int howMany)
        {
            string flickrKey = "d2845fd835868cc17fa4c1e98c03a174";
            string sharedSecret = "ea9da11a741633e9";

            Flickr flickr = new Flickr(flickrKey, sharedSecret);
            //PhotoCollection tempPhotos = flickr.PhotosGetRecent();

            int number = 0;
//             foreach (var tempPhoto in tempPhotos)
//             {
//                 _photos.Add(
//                     new Photo(number, tempPhoto.Title, tempPhoto.SmallUrl, tempPhoto.SmallUrl));
// 
//                 number++;
//                 if (number >= howMany) break;
//             }
        }
    }
}
