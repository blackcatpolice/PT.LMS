using PageTechsLMS.DataCore.Members;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Courses
{
    public class Feedback
    {
        public Guid Id { get; set; }

        public string MemberId { get; set; }

        [ForeignKey("MemberId")]
        public MemberInfo MemberInfo { get; set; }

        public Guid CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public int StartNumb { get; set; }

        public string Comment { get; set; }
    }
}
