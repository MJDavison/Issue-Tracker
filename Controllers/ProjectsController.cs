using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IssueTracker.MVC.Data;
using IssueTracker.MVC.Models;
using Microsoft.AspNetCore.Identity;
using IssueTracker.MVC.Services;
using IssueTracker.MVC.Services.Interfaces;

using IssueTracker.MVC.ViewModels.Projects;

namespace IssueTracker.MVC.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IProjectPersonnelService _projectPersonnel;
        private readonly ApplicationDbContext _context;

        private readonly UserManager<Personnel> _userManager;

        public ProjectsController(IProjectService projectService, IProjectPersonnelService projectPersonnel, ApplicationDbContext context, UserManager<Personnel> userManager)
        {
            _projectService = projectService;
            _projectPersonnel = projectPersonnel;
            _context = context;
            _userManager = userManager;
        }





        // GET: Projects
        public async Task<IActionResult> Index()
        {
            List<Project> projects = await _context.Project.ToListAsync();
            List<ProjectViewModel> ProjectVMs = new List<ProjectViewModel>();
            foreach (var project in projects)
            {
                ProjectVMs.Add(new ProjectViewModel()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description                    
                });
            }

            return View(ProjectVMs);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewBag.Tickets = _context.Tickets.Where(p => p.ProjectId == id); //.Include(p=>p.TicketUsers)

            if (project == null)
            {
                return NotFound();
            }
            DetailsViewModel viewModel = new DetailsViewModel(){
                Id = project.Id,
                Name = project.Name,
                Description = project.Description
            };


            //project = await FillLists(project);

            return View(viewModel);
        }

        // GET: Projects/Create
        public async Task<IActionResult> CreateAsync()
        {
            var personnel = await _projectPersonnel.GetAll();
            ViewData["PersonnelList"] = new SelectList(personnel, "Id", "UserName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                await _projectService.Add(project);
                
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Project project)
        {
            
            
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project.FindAsync(id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }




        public async Task<IActionResult> ManageUsers(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p=>p.ProjectUsers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            

            
            var personnel = await _projectPersonnel.GetAll();
            List<SelectListItem> personnelList = new List<SelectListItem>();
            foreach (Personnel person in personnel)
            {
                personnelList.Add(new SelectListItem(){
                    Text= person.UserName,
                    Value = person.Id,
                });
            }

            ManageProjectUsersViewModel MPUViewModel = new ManageProjectUsersViewModel(){
                Id = project.Id,
                Name = project.Name,
                Personnels = personnelList,
                ProjectUsers = project.ProjectUsers
            };
            


            return View(MPUViewModel);
        }

        public async Task<ActionResult> AddPersonnelToProject(int? id)
        {
            if(id == null)
            {
                BadRequest();
            }
            Project project = await _projectService.Get(id);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonnelToProjectAsync(int? id, List<Personnel> ProjectUsers, Project project)
        {
            if(id == null)
            {
                return BadRequest();
            }
            if(ProjectUsers.Count < 1)
            {
                return BadRequest();
            }            

            if (ModelState.IsValid)
            {
                await _projectService.Update(project);
                return RedirectToAction("Index");
            }
            return View(project);
        }


    }
}
