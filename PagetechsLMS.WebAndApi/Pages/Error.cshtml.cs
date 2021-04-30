using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PagetechsLMS.WebAndApi.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public string ExceptionMessage { get; set; }

        public int ErrorCode { get; set; } = 500;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int code)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var exceptionHandlerPathFeature =
       HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            {
                ExceptionMessage = "File error thrown";
                _logger.LogError(ExceptionMessage);
            }
            if (exceptionHandlerPathFeature?.Path == "/index")
            {
                ExceptionMessage += " from home page";
            }
            if (code != 0)
            {
                ErrorCode = code;
            }

            ExceptionMessage += exceptionHandlerPathFeature?.Error?.Message;
        }
    }
}
