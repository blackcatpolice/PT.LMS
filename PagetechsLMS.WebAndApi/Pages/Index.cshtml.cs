using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PageTechsLMS.DataCore.Courses;
using PageTechsLMS.DataCore.Members;
using PageTechsLMS.Service.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagetechsLMS.WebAndApi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        CourseExploreService courseExplore;
        public IndexModel(ILogger<IndexModel> logger, CourseExploreService _courseExplore)
        {
            _logger = logger;
            courseExplore = _courseExplore;
        }

        public List<Course> CourseList { get; set; }

        public async Task OnGet()
        {
            CourseList = await courseExplore.GetHotCourse();
        }
    }
}
