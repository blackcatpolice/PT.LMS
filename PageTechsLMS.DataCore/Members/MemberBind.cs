using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Members
{
    public class MemberBind
    {
        [Key]
        public string MemberId { get; set; }

        public MemberAccount Account { get; set; }

        public string WxNickName { get; set; }

        public string WxOpenId { get; set; }
    }
}
