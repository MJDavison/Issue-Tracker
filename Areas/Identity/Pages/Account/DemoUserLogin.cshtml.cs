using IssueTracker.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class DemoUserLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        string _ReturnUrl;
        string _Name;

        public DemoUserLoginModel(
            string ReturnUrl,
            string Name)
        {
            _ReturnUrl = ReturnUrl;
            _Name = Name;
        }
        public class InputModel
        {
            [Required]
            string userName;
        }

            [HttpPost]
        public async Task<IActionResult> DemoLogin(string returnUrl = null, string name = "")
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            var user = await _userManager.FindByNameAsync(name);
            if(user != null)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                return LocalRedirect(returnUrl);
            }
            return Page();
            }            
    }
}
