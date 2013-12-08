using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Syndication;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using FlickrNet;

namespace TestAppFlickr.Data
{
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class FlickrDataCommon : TestAppFlickr.Common.BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public FlickrDataCommon(String uniqueId, String title, String imageSmallPath, String imageLargePath)
        {
            this._uniqueId = uniqueId;
            this._title = title;
            this._imageSmallPath = imageSmallPath;
            this._imageLargePath = imageLargePath;
        }

        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        private ImageSource _large_image = null;
        private String _imageLargePath = null;
        public ImageSource LargeImage
        {
            get
            {
                if (this._large_image == null && this._imageLargePath != null)
                {
                    this._large_image = new BitmapImage(new Uri(FlickrDataCommon._baseUri, this._imageLargePath));
                }
                return this._large_image;
            }

            set
            {
                this._imageLargePath = null;
                this.SetProperty(ref this._large_image, value);
            }
        }

        private ImageSource _image = null;
        private String _imageSmallPath = null;
        public ImageSource Image
        {
            get
            {
                if (this._image == null && this._imageSmallPath != null)
                {
                    this._image = new BitmapImage(new Uri(FlickrDataCommon._baseUri, this._imageSmallPath));
                }
                return this._image;
            }

            set
            {
                this._imageSmallPath = null;
                this.SetProperty(ref this._image, value);
            }
        }

        public void SetImage(String path)
        {
            this._image = null;
            this._imageSmallPath = path;
            this.OnPropertyChanged("Image");
        }

        public override string ToString()
        {
            return this.Title;
        }
    }

    public class FlickrDataItem : FlickrDataCommon
    {
        public FlickrDataItem(String uniqueId, String title, String imageSmallPath, String imageLargePath, FlickrDataGroup group)
            : base(uniqueId, title, imageSmallPath, imageLargePath)
        {
            this._group = group;
        }

        private FlickrDataGroup _group;
        public FlickrDataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }
    }

    public class FlickrDataGroup : FlickrDataCommon
    {
        public FlickrDataGroup(String uniqueId)
            : base(uniqueId, null, null, null)
        {
            Items.CollectionChanged += ItemsCollectionChanged;
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex,Items[e.NewStartingIndex]);
                        if (TopItems.Count > 12)
                        {
                            TopItems.RemoveAt(12);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < 12 && e.NewStartingIndex < 12)
                    {
                        TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        TopItems.Add(Items[11]);
                    }
                    else if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        TopItems.RemoveAt(12);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        if (Items.Count >= 12)
                        {
                            TopItems.Add(Items[11]);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems[e.OldStartingIndex] = Items[e.OldStartingIndex];
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    TopItems.Clear();
                    while (TopItems.Count < Items.Count && TopItems.Count < 12)
                    {
                        TopItems.Add(Items[TopItems.Count]);
                    }
                    break;
            }
        }

        private ObservableCollection<FlickrDataItem> _items = new ObservableCollection<FlickrDataItem>();
        public ObservableCollection<FlickrDataItem> Items
        {
            get { return this._items; }
        }

        private ObservableCollection<FlickrDataItem> _topItem = new ObservableCollection<FlickrDataItem>();
        public ObservableCollection<FlickrDataItem> TopItems
        {
            get { return this._topItem; }
        }
    }

    public sealed class FlickrDataSource
    {
        static string flickrKey = "d2845fd835868cc17fa4c1e98c03a174";
        static string sharedSecret = "ea9da11a741633e9";



        public static readonly ObservableCollection<FlickrDataGroup> AllGroups = new ObservableCollection<FlickrDataGroup>();

        public static IEnumerable<FlickrDataGroup> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");
            
            return AllGroups;
        }

        public static FlickrDataGroup GetGroup(string uniqueId)
        {
            var matches = AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static FlickrDataItem GetItem(string uniqueId)
        {
            var matches = AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static void AddGroupForFeed()
        {
            string clearedContent = String.Empty;

			if (FlickrDataSource.GetGroup("test") != null) return;
			
            var feedGroup = new FlickrDataGroup(uniqueId: "test");

            Flickr flickr = new Flickr(flickrKey, sharedSecret);
            PhotoCollection photos = flickr.PhotosGetRecent();

            foreach (var i in photos)
            {
                string imageSmall = i.SmallUrl;
                string imageLarge = i.MediumUrl;

                
                if (imageSmall != null && feedGroup.Image == null) feedGroup.SetImage(imageSmall);
                if (imageSmall == null) imageSmall = "ms-appx:///Assets/DarkGray.png";

                feedGroup.Items.Add(new FlickrDataItem(
                    uniqueId: i.PhotoId, title: i.Title, imageSmallPath: imageSmall, imageLargePath: imageLarge, @group: feedGroup));
            }

            AllGroups.Add(feedGroup);
        }

        public FlickrDataSource()
        {
        }
    }
}
