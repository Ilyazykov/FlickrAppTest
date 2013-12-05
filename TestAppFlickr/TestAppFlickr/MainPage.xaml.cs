﻿using TestAppFlickr.Data;

using System;
using System.Collections.Generic;
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

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace TestAppFlickr
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class GroupedItemsPage : TestAppFlickr.Common.LayoutAwarePage
    {
        public GroupedItemsPage()
        {
            this.InitializeComponent();
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            RSSDataSource.AddGroupForFeedAsync("http://blogs.msdn.com/b/status/rss.aspx");
            RSSDataSource.AddGroupForFeedAsync("http://www.spugachev.com/feed");
            this.DefaultViewModel["Groups"] = RSSDataSource.AllGroups;
        }

        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemId = ((RSSDataItem)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(ItemDetailPage), itemId);
        }
    }
}