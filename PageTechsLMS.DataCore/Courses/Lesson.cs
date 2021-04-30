using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Courses
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Section> Sections { get; set; }
    }
}
