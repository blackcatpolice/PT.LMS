using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Courses
{
    public class Tags
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CourseId { get; set; } 
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}
