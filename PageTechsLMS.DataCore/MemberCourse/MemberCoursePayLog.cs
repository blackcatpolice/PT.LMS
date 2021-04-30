using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.MemberCourse
{
    public class MemberCoursePayLog
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string MemberId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
