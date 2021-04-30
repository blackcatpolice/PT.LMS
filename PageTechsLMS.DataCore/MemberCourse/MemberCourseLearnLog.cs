using PageTechsLMS.DataCore.Members;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.MemberCourse
{
    public class MemberCourseLearnLog
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid? LessonId { get; set; }
        public Guid? SectionId { get; set; }
        public Guid? SectionItemId { get; set; }
        public string MemberId { get; set; }
        [ForeignKey("MemberId")]
        public MemberAccount MemberAccount { get; set; }
        /// <summary>
        /// 剩余时长，为0 代码sectionItem 完成
        /// </summary>
        public int Remaining { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
