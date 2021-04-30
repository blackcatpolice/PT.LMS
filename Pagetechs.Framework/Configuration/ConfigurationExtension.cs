using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagetechs.Framework.Configuration
{
    public static class ConfigurationExtension
    {
        public static T Get<T>(this IConfiguration configuration, string sectionName)
        {
            return configuration.GetSection(sectionName).Get<T>();
        }
    }
}
