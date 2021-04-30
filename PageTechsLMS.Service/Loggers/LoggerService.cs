using Microsoft.Extensions.Logging;
using PageTechsLMS.Service.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Loggers
{
    public class LoggerService
    {
        public ILogger logger;
        SettingService settingService;
        public LoggerService(ILogger _logger, SettingService _settingService)
        {
            logger = _logger;
            settingService = _settingService; 
        }

    }
}
