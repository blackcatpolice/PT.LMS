using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Setting.Basic
{
    public class SiteSetting
    {
        public Guid Id { get; set; }

        public string SiteName { get; set; }

        public string Keys { get; set; }

        public string Description { get; set; }

        public string FooterScript { get; set; }

        public string FooterInfo { get; set; }


    }
}
