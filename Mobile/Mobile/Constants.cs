using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile
{
    public static class Constants
    {
        //public static string BaseApiAddress => "http://controleacceswebapi.azurewebsites.net/";
        public static string BaseApiAddress => "http://192.168.137.1:3481/";
        public const string MESSAGE_RefreshControlList = "refreshcontrollist";
        public const string MESSAGE_RefreshList = "Refreshlist";
        public const string MESSAGE_GoToDetail = "GoToDetail";
        public const string MESSAGE_FilterList = "FilterList";

        public static  bool IsLoggedIn { get; set; }

        

    }
}
