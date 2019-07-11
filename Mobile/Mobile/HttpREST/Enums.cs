using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.HttpREST
{
    public enum HttpVerbs
    {
        GET, POST
    }

    public class ContentTypes
    {
        public const string x_www_form_urlencoded = "application/x-www-form-urlencoded";
        public const string json = "application/json";
    }
}
