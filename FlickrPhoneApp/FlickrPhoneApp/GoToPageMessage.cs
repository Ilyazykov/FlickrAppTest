using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrPhoneApp.Model;

namespace FlickrPhoneApp
{
    public class GoToPageMessage
    {
        public GoToPageMessage(string _pageName, Photo _selectedPhoto)
        {
            PageName = _pageName;
            SelectedPhoto = _selectedPhoto;
        }

        public static string PageName { get; set; }
        public static Photo SelectedPhoto { get; set; }
    }
}
