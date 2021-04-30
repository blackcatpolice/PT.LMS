using PageTechsLMS.DataCore.Members;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Message
{
    public class Messagebox
    {
        public Guid Id { get; set; } 
        public string MemberId { get; set; } 
        [ForeignKey("MemberId")]
        public MemberInfo MemberInfo { get; set; } 
        public string FromeMemberId { get; set; }
        [ForeignKey("FromeMemberId")]
        public MemberInfo FromMemberInfo { get; set; } 
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsRead { get; set; }

    }
}
