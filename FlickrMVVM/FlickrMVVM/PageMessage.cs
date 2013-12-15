using FlickrMVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrMVVM
{
    class PageMessage
    {
        public System.Type PageType { get; set; }
        public static Photo MessageParameters { get; set; }
    }
}