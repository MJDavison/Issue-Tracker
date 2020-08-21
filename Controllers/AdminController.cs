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
        readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Project> FillLists(Project project)
        {
            project.AvaliablePersonnel.AllPersonnel = await _context.Users.ToListAsync();
            project.AvaliablePersonnel.Admin = await _userManager.GetUsersInRoleAsync(Data.Enums.Roles.Admin.ToString());
            project.AvaliablePersonnel.ProjectManager = await _userManager.GetUsersInRoleAsync(Data.Enums.Roles.ProjectManager.ToString());
            project.AvaliablePersonnel.Developer = await _userManager.GetUsersInRoleAsync(Data.Enums.Roles.Developer.ToString());
            project.AvaliablePersonnel.Submitter = await _userManager.GetUsersInRoleAsync(Data.Enums.Roles.Submitter.ToString());

            project.Issues = await _context.Issues.Where(i => i.ProjectId == project.ProjectId).ToListAsync();

            return project;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageRoles()
        {
            List<ApplicationUser> users = await _context.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> ManageProjectUsers()
        {
            List<Project> projects = await _context.Project.ToListAsync();           
            

            return View(projects);
        }
    }
}
