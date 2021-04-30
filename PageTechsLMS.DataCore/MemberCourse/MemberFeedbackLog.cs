using PageTechsLMS.DataCore.Members;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.MemberCourse
{
    public class MemberFeedbackLog
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string MemberId { get; set; }
        [ForeignKey("MemberId")]
        public MemberAccount MemberAccount { get; set; }
        public int StartNum { get; set; } = 0;
        public bool IsLiked { get; set; } = false;
        public bool IsFavorite { get; set; } = false;
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
