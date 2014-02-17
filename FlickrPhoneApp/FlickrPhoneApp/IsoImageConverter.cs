using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FlickrPhoneApp
{
    public class IsoImageConverter : IValueConverter
    {
        private static readonly IsolatedStorageFile Storage = IsolatedStorageFile.GetUserStoreForApplication();

        public object Convert(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            var bitmap = new BitmapImage();
            try
            {
                var path = (string)value;
                if (!String.IsNullOrEmpty(path))
                {
                    using (var file = LoadFile(path))
                    {
                        bitmap.SetSource(file);
                    }
                }
            }
            catch
            {
            }
            return bitmap;
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
