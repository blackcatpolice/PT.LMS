using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Courses
{
    public class SectionItem
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }
        public Guid LessonId { get; set; }
        public Guid SectionId { get; set; }

        public string Name { get; set; }
        public string Video { get; set; }
        public string Duration { get; set; }
        public string Level { get; set; }

        public bool IsTrailer { get; set; } 
        public bool IsFree { get; set; }
    }
}
