using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Courses
{
    public class Section
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid LessonId { get; set; }
        public string Name { get; set; }
        public List<SectionItem> Items { get; set; }
    }
}
