using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Setting.File
{
    public class FileSetting
    {
        [Comment("File Type : LocalFilebase,Qiniu,AliOss")]
        public FileType Use { get; set; }

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public string Bucket { get; set; }

        public string Zone { get; set; }

        public string Url { get; set; }
    }

    public enum FileType
    {
        Local,
        Qiniu,
        AliOss
    }
}
