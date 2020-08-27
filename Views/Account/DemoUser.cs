using IssueTracker.MVC.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Views.Account
{
    public class DemoUser
    {
        readonly SignInManager<Personnel> _signInManager;
        readonly UserManager<Personnel> _userManager;

        public DemoUser(SignInManager<Personnel> signInManager, UserManager<Personnel> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;


        }

    }
}
