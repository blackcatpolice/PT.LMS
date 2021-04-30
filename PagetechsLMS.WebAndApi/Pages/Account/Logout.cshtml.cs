using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PageTechsLMS.DataCore.Members;

namespace PagetechsLMS.WebAndApi.Pages.Account
{
    public class LogoutModel : PageModel
    {
        SignInManager<MemberAccount> signInManager;
        public LogoutModel(SignInManager<MemberAccount> _signInManager)
        {
            signInManager = _signInManager;
        }
        public async Task OnGet()
        {
            await signInManager.SignOutAsync();
        }
    }
}
