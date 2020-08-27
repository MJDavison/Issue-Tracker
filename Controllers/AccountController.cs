using IssueTracker.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        readonly UserManager<Personnel> userManager;
        readonly SignInManager<Personnel> signInManager;

        public AccountController(UserManager<Personnel> userManager, SignInManager<Personnel> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            
        }

        public string IssueStatus()
        {
            string query = "John Doe";
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["username"]))
                query = HttpContext.Request.Query["username"].ToString();


            return query;
        }

        public async Task LoginDemoUser(string username)
        {
            string normalizedUsername = username.Normalize().ToUpper(); ;
            Personnel DemoUser = await userManager.FindByNameAsync(normalizedUsername);

            await signInManager.SignInAsync(DemoUser, true);
            RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Demo_Login()
        {
            return View();
        }

        public async Task<IActionResult> DemoUserAsync()
        {
            string query = IssueStatus();
            switch (query)
            {
                case "DemoAdmin":
                    await LoginDemoUser(query);
                    break;
                case "DemoPM":
                    await LoginDemoUser(query);
                    break;
                case "DemoDev":
                    await LoginDemoUser(query); 
                    break;
                case "DemoSubmitter":
                    await LoginDemoUser(query);
                    break;

                default:
                    break;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
