using Pagetechs.Framework.Dtos.ModolHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageTechsLMS.Admin.Dtos.Course
{
    public class DTOSectionInput
    {
        [ModelType(ControlType = ControlType.Hidden)]
        public Guid? Id { get; set; }
        [ModelType(ControlType = ControlType.Hidden)]
        public Guid CourseId { get; set; }
        [ModelType(ControlType = ControlType.Hidden)]
        public Guid LessonId { get; set; }
        [ModelType(Name = "部分名", ControlType = ControlType.Input)]
        public string Name { get; set; }
    }
}
