﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.API.IN
{
    public class SaveUserRolesModel
    {
        public string userId { get; set; }
        public List<string> rolesList { get; set; }
    }
}
