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
    public abstract class RSSDataCommon : TestAppFlickr.Common.BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public RSSDataCommon(String uniqueId, String title, String imagePath)
        {
            this._uniqueId = uniqueId;
            this._title = title;
            this._imagePath = imagePath;
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

        private string _subtitle = string.Empty;
        public string Subtitle
        {
            get { return this._subtitle; }
            set { this.SetProperty(ref this._subtitle, value); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private ImageSource _image = null;
        private String _imagePath = null;
        public ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    this._image = new BitmapImage(new Uri(RSSDataCommon._baseUri, this._imagePath));
                }
                return this._image;
            }

            set
            {
                this._imagePath = null;
                this.SetProperty(ref this._image, value);
            }
        }

        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.OnPropertyChanged("Image");
        }

        public override string ToString()
        {
            return this.Title;
        }
    }

    public class RSSDataItem : RSSDataCommon
    {
        public RSSDataItem(String uniqueId, String title, String imagePath, RSSDataGroup group)
            : base(uniqueId, title, imagePath)
        {
            this._group = group;
        }

        private RSSDataGroup _group;
        public RSSDataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }
    }

    public class RSSDataGroup : RSSDataCommon
    {
        public RSSDataGroup(String uniqueId, String title, String imagePath)
            : base(uniqueId, title, imagePath)
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

        private ObservableCollection<RSSDataItem> _items = new ObservableCollection<RSSDataItem>();
        public ObservableCollection<RSSDataItem> Items
        {
            get { return this._items; }
        }

        private ObservableCollection<RSSDataItem> _topItem = new ObservableCollection<RSSDataItem>();
        public ObservableCollection<RSSDataItem> TopItems
        {
            get {return this._topItem; }
        }
    }

    public sealed class RSSDataSource
    {
        public static readonly ObservableCollection<RSSDataGroup> AllGroups = new ObservableCollection<RSSDataGroup>();

        public static IEnumerable<RSSDataGroup> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");
            
            return AllGroups;
        }

        public static RSSDataGroup GetGroup(string uniqueId)
        {
            var matches = AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static RSSDataItem GetItem(string uniqueId)
        {
            var matches = AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<bool> AddGroupForFeedAsync(string feedUrl)
        {
            string clearedContent = String.Empty;

            if (RSSDataSource.GetGroup(feedUrl) != null) return false;

            var feed = await new SyndicationClient().RetrieveFeedAsync(new Uri(feedUrl));

            var feedGroup = new RSSDataGroup(
                uniqueId: feedUrl,
                title: feed.Title != null ? feed.Title.Text : null,
                imagePath: feed.ImageUri != null ? feed.ImageUri.ToString() : null);

            foreach (var i in feed.Items)
            {
                string imagePath = GetImageFromPostContents(i);

                if (imagePath != null && feedGroup.Image == null) feedGroup.SetImage(imagePath);
                if (imagePath == null) imagePath = "ms-appx:///Assets/DarkGray.png";

                feedGroup.Items.Add(new RSSDataItem(
                    uniqueId: i.Id, title: i.Title.Text, imagePath: imagePath, @group: feedGroup));
            }

            AllGroups.Add(feedGroup);
            return true;
        }

        private static string GetImageFromPostContents(SyndicationItem item)
        {
            string text2search = "";

            if (item.Content != null) text2search += item.Content.Text;
            if (item.Summary != null) text2search += item.Summary.Text;

            return Regex.Matches(text2search,
                @"(?<=<img\s+[^>]*?src=(?<q>['""]))(?<url>.+?)(?=\k<q>)",
                RegexOptions.IgnoreCase)
            .Cast<Match>()
            .Where(m =>
            {
                Uri url;
                if (Uri.TryCreate(m.Groups[0].Value, UriKind.Absolute, out url))
                {
                    string ext = Path.GetExtension(url.AbsolutePath).ToLower();
                    if (ext == ".png" || ext == ".jpg" || ext == ".bmp") return true;
                }
                return false;
            })
            .Select(m => m.Groups[0].Value)
            .FirstOrDefault();
        }

        public RSSDataSource()
        {
        }
    }
}
