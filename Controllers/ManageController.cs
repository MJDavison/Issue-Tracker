using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IssueTracker.MVC.Controllers
{
    public class ManageController : Controller
    {
        private readonly UserManager<Personnel> _userManager;
        private readonly SignInManager<Personnel> _signInManager;
        private readonly ILogger<ManageController> _logger;

        public ManageController(UserManager<Personnel> userManager, SignInManager<Personnel> signInManager, ILogger<ManageController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Profile";
            Personnel user = await _userManager.GetUserAsync(User);

            ViewModels.Account.AccountInfoViewModel userVM = new ViewModels.Account.AccountInfoViewModel()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserRole = user.UserRole,

                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber

            };
            return View(userVM);
        }
    }
}
