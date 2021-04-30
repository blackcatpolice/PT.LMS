using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageTechsLMS.Admin.Controllers.Finance
{
    public class TradeStatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
