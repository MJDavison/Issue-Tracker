using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.MVC.Data;
using IssueTracker.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.MVC.Controllers
{
    public class AdminController : Controller
    {

        readonly ApplicationDbContext _context;
        readonly UserManager<Personnel> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<Personnel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageRoles()
        {
            List<Personnel> users = await _context.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> ManageProjectUsers()
        {
            List<Project> projects = await _context.Project.ToListAsync();           
            

            return View(projects);
        }
    }
}
