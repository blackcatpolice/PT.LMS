using PageTechsLMS.DataCore.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Courses
{
    public class Comments
    {
        public Guid Id { get; set; }


        public string MemberId { get; set; }

        public MemberAccount MemberAccount { get; set; }

        public string Content { get; set; }



        public DateTime CreateTime { get; set; }
    }
}
