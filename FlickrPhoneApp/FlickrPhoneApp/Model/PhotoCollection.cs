using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net.NetworkInformation;
using System.Windows;

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
            if (NetworkInterface.GetIsNetworkAvailable())
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

                //WriteSettingToIsolatedStorage
                if (IsolatedStorageFile.GetUserStoreForApplication().FileExists("flickrParameters.txt"))
                {
                    using (var file = IsolatedStorageFile.GetUserStoreForApplication().CreateFile("flickrParameters.txt"))
                    {
                        using (var fileWriter = new StreamWriter(file))
                        {
                            foreach (var photo in _photos)
                            {
                                fileWriter.WriteLine(photo.ID);
                                fileWriter.WriteLine(photo.Title);
                                fileWriter.WriteLine(photo.SmallUrl);
                                fileWriter.WriteLine(photo.LargeUrl);
                            }
                            fileWriter.WriteLine("end");
                        }
                    }
                }
            }
            else
            {
                using (var file = IsolatedStorageFile.GetUserStoreForApplication().OpenFile("flickrParameters.txt", FileMode.Open))
                {
                    using (var fileReader = new StreamReader(file))
                    {
                        string id = fileReader.ReadLine();
                        while (id != "end")
                        {
                            string title = fileReader.ReadLine();
                            string smallUrl = fileReader.ReadLine();
                            string largeUrl = fileReader.ReadLine();

                            _photos.Add(
                                new Photo(Convert.ToInt32(id), title, smallUrl, largeUrl));

                            id = fileReader.ReadLine();
                        }
                        


                        //_photos.Add(
                        //    new Photo(number, tempPhoto.Title, tempPhoto.SmallUrl, tempPhoto.SmallUrl));
                    }
                }
            }
        }
    }
}
