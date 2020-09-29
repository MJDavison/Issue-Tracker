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
using IssueTracker.MVC.Repository.Interfaces;

namespace IssueTracker.MVC.Controllers
{
    public class AdminController : Controller
    {
         
        readonly ApplicationDbContext _context;
        readonly UserManager<Personnel> _userManager;

        readonly IUserRepository _UserRepository;
        readonly IProjectRepository _ProjectRepository;

        public AdminController(ApplicationDbContext context, UserManager<Personnel> userManager, IUserRepository userRepository, IProjectRepository projectRepository)
        {
            _context = context;
            _userManager = userManager;
            _UserRepository = userRepository;
            _ProjectRepository = projectRepository;
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
                                  //Change their RoleGroup
                                  user.UserRole = rolename;
                                  _UserRepository.Update(user);
                                  await _UserRepository.Save();                                
                              }          
                }                
               return RedirectToAction("ManageRoles");
        }
        [HttpGet]
        public async Task<IActionResult> ManageProjectUsers()
        {
            IList<Project> projects = await _context.Project.ToListAsync();   
            IList<Personnel> personnels = (IList<Personnel>)await _UserRepository.GetAll();

            var projectListItems = projects.Select(p=> new SelectListItem {
                Text = p.Name, Value = p.Id.ToString()}).ToList();

            var adminListItems = personnels.Where(u=>u.UserRole == "Admin").Select(u=> new SelectListItem{
                Text = u.UserName, Value = u.Id}).ToList();

            var developerListItems = personnels.Where(u=>u.UserRole == "Developer").Select(u=> new SelectListItem{
                Text = u.UserName, Value = u.Id}).ToList();

            var projectManagerListItems = personnels.Where(u=>u.UserRole == "Project Manager").Select(u=> new SelectListItem{
                Text = u.UserName, Value = u.Id}).ToList();

            var submitterListItems = personnels.Where(u=>u.UserRole == "Submitter").Select(u=> new SelectListItem{
                Text = u.UserName, Value = u.Id}).ToList();

            ManageProjectUsersViewModel mpuViewModel = new ManageProjectUsersViewModel(){
                Projects = projectListItems,
                Admins = adminListItems,
                Developers = developerListItems,
                ProjectManagers = projectManagerListItems,
                Submitters = submitterListItems
            };            
            return View(mpuViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ManageProjectUsers(ManageProjectUsersViewModel mpuViewModel){
            //Assign the users (Ids stored in mpuViewModel) to all the projects selected.

            foreach (int id in mpuViewModel.ProjectIds)
            {
                List<string> userIds = new List<string>();
                Project currentProject = await _context.Project.Include(p=>p.ProjectUsers).Where(p=>p.Id ==id).FirstOrDefaultAsync();
                    userIds.Add(mpuViewModel.AdminId);
                    userIds.Add(mpuViewModel.ProjectManagerId);
                    

                foreach (string item in mpuViewModel.DeveloperIds)
                {
                    userIds.Add(item);                            
                }
                
                
                foreach (string item in mpuViewModel.SubmitterIds)
                {
                    userIds.Add(item);
                }
                currentProject.ProjectUsers.Clear(); 
                foreach(string Id in userIds){                   
                    
                    currentProject.ProjectUsers.Add(new ProjectUser{
                        PersonnelId = Id
                    });
                }
                
                   _ProjectRepository.UpdateProjectUsers(currentProject);
                   await _ProjectRepository.Save();
            }
           return RedirectToAction("ManageProjectUsers");
        }
    }
}
