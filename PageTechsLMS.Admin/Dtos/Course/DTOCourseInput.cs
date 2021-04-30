using Pagetechs.Framework.Dtos.ModolHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageTechsLMS.Admin.Dtos
{
    public class DTOCourseInput
    {
        [ModelType(ControlType = ControlType.Hidden)]
        public Guid? Id { get; set; }
        [ModelType(Name = "课程分类", ControlType = ControlType.List, DataSourceTable = "Category", DataSourceTableValue = "Name")]
        public Guid CategoryId { get; set; }
        [ModelType(Name = "课程名")]
        public string Name { get; set; }
        [ModelType(Name = "描述")]
        public string Description { get; set; }
        [ModelType(Name = "封面", ControlType = ControlType.Img)]
        public string Cover { get; set; }
        [ModelType(Name = "图标", ControlType = ControlType.Img)]
        public string Icon { get; set; }
        [ModelType(Name = "视频", ControlType = ControlType.Video)]
        public string Video { get; set; }
        [ModelType(Name = "内容", ControlType = ControlType.TextArea_Editor)]
        public string Content { get; set; }
        [ModelType(Name = "价格", ControlType = ControlType.Number)]
        public double Price { get; set; }
        [ModelType(Name = "点赞数", ControlType = ControlType.Number)]
        public int Like { get; set; }
        [ModelType(Name = "收藏", ControlType = ControlType.Number)]
        public int Favorite { get; set; }
        [ModelType(Name = "浏览数", ControlType = ControlType.Number)]
        public int ViewNumb { get; set; }
        [ModelType(Name = "星数", ControlType = ControlType.Number)]
        public int StartNum { get; set; }
        [ModelType(Name = "课程级别", ControlType = ControlType.Input)]
        public string Level { get; set; }
        [ModelType(Name = "标签", ControlType = ControlType.Tags)]
        public List<string> Tags { get; set; }
        [ModelType(Name = "排序", ControlType = ControlType.Number)]
        public int Order { get; set; }
        [ModelType(Name = "创建时间", ControlType = ControlType.Date)]
        public DateTime CreateTime { get; set; }
        [ModelType(Name = "热门", ControlType = ControlType.Switch)]
        public bool IsHot { get; set; }
    }
}
