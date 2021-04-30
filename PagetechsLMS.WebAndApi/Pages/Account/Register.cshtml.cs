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
    public class RegisterModel : PageModel
    {
        UserManager<MemberAccount> userManager;
        public RegisterModel(UserManager<MemberAccount> _userManager)
        {
            userManager = _userManager;
        }

        public void OnGet()
        {
        }

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }

        public async Task<IActionResult> OnPost(RegisterInputModel registerInput)
        {
            try
            {

                if (registerInput.Passworld == registerInput.RePassword)
                {
                    ErrorMessage = "两次输入密码不一致";
                    return Page();

                }

                var result = await userManager.CreateAsync(new MemberAccount
                {
                    UserName = registerInput.UserName,
                }, registerInput.Passworld);

                if (result.Succeeded)
                {
                    return Redirect("/");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ErrorMessage += item.Code + " " + item.Description + '\n'; 
                    }
                }
            }
            catch (Exception exc)
            {

                throw;
            }

            return Page();
        }
    }

    public class RegisterInputModel
    {
        public string UserName { get; set; }
        public string Passworld { get; set; }
        public string RePassword { get; set; }
    }
}
