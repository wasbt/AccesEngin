using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Model
{
    public class LoginModel
    {
        public string GrantType { get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
