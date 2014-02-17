using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows;

using FlickrNet;
using Microsoft.Phone.Net.NetworkInformation;

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
            if (IsOnline())
            {
                GetPhotosFromFlickr(12);
                WriteFlickrParametersToIsolatedStorage();
            }
            else
            {
                ReadFlickrParametersFromIsolatedStorage();
            }
        }

        private bool IsOnline()
        {
            bool isNetworkAvailable = Microsoft.Phone.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            NetworkInterfaceType type = Microsoft.Phone.Net.NetworkInformation.NetworkInterface.NetworkInterfaceType;

            if (isNetworkAvailable)
            {
                if (type == NetworkInterfaceType.Wireless80211)
                    return true;
            }

            return false;
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

        private void WriteFlickrParametersToIsolatedStorage()
        {
            if (IsolatedStorageFile.GetUserStoreForApplication().FileExists("flickrParameters.txt") == false)
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

        private void ReadFlickrParametersFromIsolatedStorage()
        {
            if (IsolatedStorageFile.GetUserStoreForApplication().FileExists("flickrParameters.txt"))
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
                    }
                }
            }
        }
    }
}