using System.Windows;
using System.Collections.ObjectModel;
using FlickrNet;


namespace FlickrPhoneApp.Model
{
    public class MyPhotoCollection
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

            flickr.PhotosGetRecentAsync(r =>
            {
                PhotoCollection tempPhotos = r.Result;

                int number = 0;
                foreach (var tempPhoto in tempPhotos)
                {
                    _photos.Add(
                        new Photo(number, tempPhoto.Title, tempPhoto.SmallUrl, tempPhoto.SmallUrl));
                
                    number++;
                    if (number >= howMany) break;
                }

            });
        }
    }
}
