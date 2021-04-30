using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.File
{
    public class FilebaseInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string PhysicPath { get; set; }

        public string RelativePath { get; set; }

        public string Ext { get; set; }

        public string Size { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
