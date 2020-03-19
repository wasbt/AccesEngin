using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile
{
    public class AppUrls
    {
#if DEBUG
        public const string BaseUrl = "https://myopsapi.azurewebsites.net/";
        //public const string BaseUrl = "http://192.168.1.118:6028";
#else
    public const string BaseUrl = "https://myopsapi.ocpgroup.ma";
#endif


        public const string Login = BaseUrl + "/Token";

    }
}
