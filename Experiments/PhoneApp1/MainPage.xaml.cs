using Microsoft.Phone.Controls;

using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;

using System.Windows.Media.Imaging;
using System.Windows.Resources;
using Microsoft.Phone.Tasks;
using System.Net;


namespace PhoneApp1
{


    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        void webClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            int count;
            Stream stream = e.Result;
            byte[] buffer = new byte[1024];
            using (var file = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var fileStream = new IsolatedStorageFileStream("logo.jpg", FileMode.Create, file))
                {
                    count = 0;

                    while (0 < (count = stream.Read(buffer, 0, buffer.Length)))
                    {
                        fileStream.Write(buffer, 0, count);
                    }

                    stream.Close();
                    fileStream.Close();
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            WebClient m_webClient = new WebClient();
            Uri m_uri = new Uri("http://iremind.bamtoo.com/images/google_logo.png");
            m_webClient.OpenReadCompleted += new OpenReadCompletedEventHandler(webClient_OpenReadCompleted);
            m_webClient.OpenReadAsync(m_uri);
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bi = new BitmapImage();

            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile("logo.jpg", FileMode.Open, FileAccess.Read))
                {
                    bi.SetSource(fileStream);
                    this.img.Height = bi.PixelHeight;
                    this.img.Width = bi.PixelWidth;
                }
            }
            this.img.Source = bi;
        }

        private void btnMedia_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}