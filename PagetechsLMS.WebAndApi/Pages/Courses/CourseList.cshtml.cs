using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PageTechsLMS.DataCore.Courses;
using PageTechsLMS.Service.Courses;

namespace PagetechsLMS.WebAndApi.Pages.Courses
{
    public class CourseListModel : PageModel
    {
        CourseExploreService exploreService;
        public List<Course> Courses { get; set; }
        public CourseListModel(CourseExploreService _exploreService)
        {
            exploreService = _exploreService;
        }
        public async Task OnGet()
        {
            Courses = await exploreService.GetAllCourseList();
        }
    }
}
