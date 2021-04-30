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
    public class LoginModel : PageModel
    {
        SignInManager<MemberAccount> signInManager;
        UserManager<MemberAccount> userManager;
        public LoginModel(SignInManager<MemberAccount> _signInManager, UserManager<MemberAccount> _userManager)
        {
            signInManager = _signInManager;
            userManager = _userManager;
        }

        public void OnGet()
        {

        }

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }

        public async Task<IActionResult> OnPost(LoginInputModel loginModel)
        {
            var result = await signInManager.PasswordSignInAsync(loginModel.UserName,
               loginModel.Password,
                loginModel.Persistent,
                false);

            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(loginModel.UserName);

                var nickname = user.NickName ?? user.UserName;
                await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("nickName", nickname));
                await signInManager.SignInAsync(user, false);
                return Redirect("/");
            }
            else
            {
                ErrorMessage = "用户名或密码错误！";
            }
            return Page();
        }
    }

    public class LoginInputModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool Persistent { get; set; }
    }
}
