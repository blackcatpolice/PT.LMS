using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Members
{
    public class MemberAccount : IdentityUser
    {
        public string MemberInfoId { get; set; }

        [ForeignKey("MemberInfoId")]
        public MemberInfo MemberInfo { get; set; }

        public string MemberBindId { get; set; }

        [ForeignKey("MemberBindId")]
        public MemberBind MemberBind { get; set; }

        public string NickName { get; set; }
    }
}
