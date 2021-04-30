using PageTechsLMS.DataCore.Members;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Community
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid TopicId { get; set; }
        [ForeignKey("TopicId")]
        public Topics Topics { get; set; }
        public string MemberId { get; set; }
        [ForeignKey("MemberId")]
        public MemberInfo MemberInfo { get; set; }
        public DateTime CreateTime { get; set; }
        public string Content { get; set; }
        public int LikeNum { get; set; }
        public int FavoriteNum { get; set; }
    }
}
