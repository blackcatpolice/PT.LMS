using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.FastAdmin.Antd.IdentityServer.Model
{
    public class UserConfigOptions
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
