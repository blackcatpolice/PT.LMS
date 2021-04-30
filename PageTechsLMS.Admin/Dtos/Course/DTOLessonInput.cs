using Pagetechs.Framework.Dtos.ModolHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageTechsLMS.Admin.Dtos.Course
{
    public class DTOLessonInput
    {
        [ModelType(ControlType = ControlType.Hidden)]
        public Guid? Id { get; set; }
        [ModelType(ControlType = ControlType.Hidden)]
        public Guid CourseId { get; set; }
        [ModelType(Name = "单元名", ControlType = ControlType.Input)]
        public string Name { get; set; }
        [ModelType(Name = "单元描述 ", ControlType = ControlType.Input)]
        public string Description { get; set; }
    }
}
