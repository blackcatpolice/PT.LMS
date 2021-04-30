using Pagetechs.Framework.Dtos.ModolHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageTechsLMS.Admin.Dtos
{
    public class DTOCategoryInput
    {
        [ModelType(ControlType = ControlType.Hidden)]
        public Guid? Id { get; set; }
        [ModelType(Name = "名称")]
        public string Name { get; set; }
        [ModelType(Name = "描述")]
        public string Description { get; set; }
        [ModelType(Name = "封面")]
        public string Cover { get; set; }
    }
}
