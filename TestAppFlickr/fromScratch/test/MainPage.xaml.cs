using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FlickrNet;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace test
{
    public class FlickrData
    {
        public string Title { get; set; }
        public string imageUriSmall { get; set; }
        public string imageUriLarge { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<FlickrData> flickrImages;

        public MainPage()
        {
            this.InitializeComponent();

            string flickrKey = "d2845fd835868cc17fa4c1e98c03a174";
            string sharedSecret = "ea9da11a741633e9";

            Flickr flickr = new Flickr(flickrKey, sharedSecret);
            PhotoCollection photos = flickr.PhotosGetRecent();

            flickrImages = new ObservableCollection<FlickrData>();

            foreach (var i in photos)
            {
                flickrImages.Add(new FlickrData { Title = i.Title, imageUriSmall = i.SmallUrl, imageUriLarge = i.LargeUrl });
            }

            gvMain.ItemsSource = flickrImages;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
