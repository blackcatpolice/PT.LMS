using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Courses
{
    public class Course
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cover { get; set; }
        public string Icon { get; set; }
        public string Video { get; set; }
        public string Content { get; set; }
        public double Price { get; set; }
        public int Like { get; set; }
        public int CommentNum { get; set; }
        public int Favorite { get; set; }
        public int ViewNumb { get; set; }
        public int StartNum { get; set; }
        public string Level { get; set; }
        public List<Tags> Tags { get; set; }
        public int Order { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsHot { get; set; }

    }
}
