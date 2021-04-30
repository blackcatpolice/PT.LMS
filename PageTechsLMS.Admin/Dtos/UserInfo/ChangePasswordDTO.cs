using Pagetechs.Framework.Dtos.ModolHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageTechsLMS.Admin.Dtos.UserInfo
{
    public class ChangePasswordDTO
    {
        [ModelType(Name = "旧密码")]
        public string OldPassword { get; set; }
        [ModelType(Name = "新密码")]
        public string NewPassword { get; set; }
        [ModelType(Name = "重复新密码")] 
        public string RepeatNewPassword { get; set; }
    }
}
