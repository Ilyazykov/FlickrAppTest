using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FlickrPhoneApp
{
    public class IsoImageConverter : IValueConverter
    {
        private static readonly IsolatedStorageFile Storage = IsolatedStorageFile.GetUserStoreForApplication();
        
        public static string StorageFolder = "ImageCache";


        public object Convert(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            var path = value as string;
            if (String.IsNullOrEmpty(path)) return null;

            var imageFileUri = new Uri(path);
            if (imageFileUri.Scheme == "http" || imageFileUri.Scheme == "https")
            {
                if (!NetworkInterface.GetIsNetworkAvailable())
                {
                    if (Storage.FileExists(GetFileNameInIsolatedStorage(imageFileUri)))
                    {
                        return ExtractFromLocalStorage(imageFileUri);
                    }
                    return LoadDefaultIfPassed(imageFileUri, (parameter ?? string.Empty).ToString());
                }
                return DownloadFromWeb(imageFileUri);
            }

            var bitmap = new BitmapImage(imageFileUri);
            return bitmap;
        }

        private static object DownloadFromWeb(Uri imageFileUri)
        {
            var webClient = new WebClient();

            var bitmap = new BitmapImage();

            webClient.OpenReadCompleted += (o, e) =>
            {
                if (e.Error != null || e.Cancelled) return;

                WriteToIsolatedStorage(IsolatedStorageFile.GetUserStoreForApplication(), e.Result,
                    GetFileNameInIsolatedStorage(imageFileUri));

                try
                {
                    string isolatedStoragePath = GetFileNameInIsolatedStorage(imageFileUri);
                    using (var file = Storage.OpenFile(isolatedStoragePath, FileMode.Open, FileAccess.Read))
                    {
                        bitmap.SetSource(file);
                    }
                }
                catch (Exception)
                {
                }

                e.Result.Close();
            };
            webClient.OpenReadAsync(imageFileUri);
            return bitmap;
        }

        private static string GetFileNameInIsolatedStorage(Uri imageFileUri)
        {
            return StorageFolder + "\\" + imageFileUri.AbsoluteUri.GetHashCode() + ".img";
        }

        private static void WriteToIsolatedStorage(IsolatedStorageFile file, Stream stream, string fileName)
        {
            IsolatedStorageFileStream outputStream = null;
            try
            {
                if (!file.DirectoryExists(StorageFolder))
                {
                    file.CreateDirectory(StorageFolder);
                }
                if (file.FileExists(fileName))
                {
                    file.DeleteFile(fileName);
                }
                outputStream = file.CreateFile(fileName);

                var buffer = new byte[32768];
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, read);
                }
                outputStream.Close();
            }
            catch
            {
                if (outputStream != null) outputStream.Close();
            }
        }

        private object ExtractFromLocalStorage(Uri imageFileUri)
        {
            string path = GetFileNameInIsolatedStorage(imageFileUri);

            using (var file = Storage.OpenFile(path, FileMode.Open, FileAccess.Read))
            {
                var bitmap = new BitmapImage();
                bitmap.SetSource(file);
                return bitmap;
            }
        }

        private object LoadDefaultIfPassed(System.Uri imageFileUri, string defaultImagePath)
        {
            var defaultImageUri = (defaultImagePath ?? string.Empty);
            if (!string.IsNullOrEmpty(defaultImageUri))
            {
                var bm = new BitmapImage(new Uri(defaultImageUri, UriKind.Relative));         //Load default Image
                return bm;
            }
            else
            {
                var bm = new BitmapImage(imageFileUri);
                return bm;
            }
        }

        private Stream LoadFile(string file)
        {
            using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                return isoStore.OpenFile(file, FileMode.Open, FileAccess.Read);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }



    }
}
