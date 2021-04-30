using Pagetechs.Framework.Dtos.ModolHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageTechsLMS.Admin.Dtos.Course
{
    public class DTOSectionItemInput
    {
        [ModelType(ControlType = ControlType.Hidden)]
        public Guid? Id { get; set; }
        [ModelType(ControlType = ControlType.Hidden)]
        public Guid CourseId { get; set; }
        [ModelType(ControlType = ControlType.Hidden)]
        public Guid LessonId { get; set; }
        [ModelType(ControlType = ControlType.Hidden)]
        public Guid SectionId { get; set; }
        [ModelType(Name = "视频名", ControlType = ControlType.Input)]
        public string Name { get; set; }

        [ModelType(Name = "视频地址", ControlType = ControlType.Video)]
        public string Video { get; set; }
        [ModelType(Name = "描述", ControlType = ControlType.Input)]
        public string Duration { get; set; }
        [ModelType(Name = "等级", ControlType = ControlType.Input)]
        public string Level { get; set; }
        [ModelType(Name = "是否试听", ControlType = ControlType.CheckBox)]
        public bool IsTrailer { get; set; }
        [ModelType(Name = "是否免费", ControlType = ControlType.CheckBox)]
        public bool IsFree { get; set; }
    }
}
