using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.MVC.Data;
using IssueTracker.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IssueTracker.MVC.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [HttpGet]
        public async Task<IActionResult> ManageRoles()
        {
            List<Personnel> users = await _context.Users.ToListAsync();
            List<SelectListItem> PersonnelList = new List<SelectListItem>();

            foreach (Personnel user in users)
            {
                PersonnelList.Add(new SelectListItem(){
                    Value = user.Id,
                    Text = user.UserName,
                });
            }
            ManageRolesViewModel mrViewModel = new ManageRolesViewModel(){
                Personnel = PersonnelList,                
                roleId = 0,
            };


            return View(mrViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ManageRoles(ManageRolesViewModel mrViewModel)
        {
                if(ModelState.IsValid){
                    IList<string> roleNames = null;
                    
                              foreach (string id in mrViewModel.PersonnelIdsToChange)
                              {
                                  Personnel user = await _userManager.FindByIdAsync(id);
                                  roleNames = await _userManager.GetRolesAsync(user);
                                  await _userManager.RemoveFromRolesAsync(user, roleNames);
                                  string rolename = Enum.GetName(typeof(Enums.Roles),mrViewModel.roleId);
                                  await _userManager.AddToRoleAsync(user, rolename);

                              }          
                }
                return View(mrViewModel);
        }

        public async Task<IActionResult> ManageProjectUsers()
        {
            List<Project> projects = await _context.Project.ToListAsync();           
            

            return View(projects);
        }
    }
}
