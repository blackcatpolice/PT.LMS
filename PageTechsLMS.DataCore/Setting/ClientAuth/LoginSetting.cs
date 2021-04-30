using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Setting.ClientAuth
{
    public class LoginSetting
    {
        /// <summary>
        /// Client login type , Josn schema:['phone','wx']
        /// </summary>
        [Comment("Client login type , Josn schema:['phone','wx']")]
        public string LoginTypes { get; set; }
    }
}
