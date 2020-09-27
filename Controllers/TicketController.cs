using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IssueTracker.MVC.Data;
using IssueTracker.MVC.Models;
using System.Collections;
using IssueTracker.MVC.Services;
using IssueTracker.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using IssueTracker.MVC.ViewModels.Tickets;
using IssueTracker.MVC.ViewModels;

namespace IssueTracker.MVC.Controllers
{
    public class TicketController : Controller
    {

        private readonly ITicketService _ticketService;
        private readonly ITicketPersonnelService _ticketPersonnel;

        private readonly IProjectService _projectService;
        private readonly ApplicationDbContext _context;

        private readonly UserManager<Personnel> _userManager;

        

        [BindProperty]
        public Ticket Ticket { get; set; }
        [BindProperty]
        public List<TicketUser> TicketUsers { get; set; }

        public TicketController(ITicketService ticketService, ITicketPersonnelService ticketPersonnel, ApplicationDbContext context, UserManager<Personnel> userManager, IProjectService projectService)
        {
            _ticketService = ticketService;
            _ticketPersonnel = ticketPersonnel;
            _context = context;
            _userManager = userManager;
            _projectService = projectService;
        }

        public bool IssueStatus()
        {
            string query = "";
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["status"]))
                query = HttpContext.Request.Query["status"].ToString();

            return query switch
            {
                "open" => true,
                "closed" => false,
                _ => true,
            };
        }

        
        // GET: Issue
        public async Task<IActionResult> Index()
        {

            bool isOpen = IssueStatus();
            //List<Ticket> openIssues = await _context.Tickets.Where(x => x.IsSolved).ToListAsync();
            //List<Ticket> closedIssues = await _context.Tickets.Where(x => x.IsSolved == false).ToListAsync();
            /*switch (isOpen)
            {
                case true:
                    return View(openIssues);
                case false:
                    return View(closedIssues);
                
            }*/
            List<Ticket> Tickets = await _context.Tickets.ToListAsync();

            List<TicketViewModel> TicketVMs = new List<TicketViewModel>();

            foreach (Ticket ticket in Tickets)
            {
                TicketVMs.Add(new TicketViewModel()
                {   
                    Id = ticket.Id,             
                    Title = ticket.Title,
                    Comment = ticket.Comment,
                    PostDate = ticket.PostDate,
                    ProjectId = ticket.ProjectId
                });
            }
            return View(TicketVMs);
        }

        public async Task<IActionResult> UpdateIssueStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueModel = await _context.Tickets.FindAsync(id);
            if (issueModel == null)
            {
                return NotFound();
            }
            return View(issueModel);



        }

        // GET: Issue/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var currentTicket = await _context.Tickets.Include(t=>t.Project)
                .FirstOrDefaultAsync(m =>m.Id == id);
            if (currentTicket == null)
            {
                return NotFound();
            }

            
            DetailsViewModel dViewModel = new DetailsViewModel(){
                Id = currentTicket.Id,
                Title = currentTicket.Title,
                Comment = currentTicket.Comment,
                Priority = currentTicket.Priority,
                Type = currentTicket.Type,
                Status = currentTicket.Status,
                ProjectName = currentTicket.Project.Name,
                AuthorId = currentTicket.AuthorId,
                DeveloperId = currentTicket.DeveloperId,           
            };
            

            return View(dViewModel);
        }

        // GET: Issue/Create
        public async Task<IActionResult> Create()
        {
            var personnel = await _ticketPersonnel.GetAll();
            ViewData["PersonnelList"] = new SelectList(personnel, "Id", "UserName");
            string stringProjectID = "";
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["project-id"]))
                stringProjectID = HttpContext.Request.Query["project-id"].ToString();            
            
            int projectId = int.Parse(stringProjectID);
            ViewData["project-id"] = projectId;
            return View();
        }

        // POST: Issue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel ticket)
        {
            List<TicketUser> ListOfUsers = new List<TicketUser>();
            Personnel Author = await _userManager.FindByNameAsync(User.Identity.Name);
            ListOfUsers.Add(new TicketUser(){
                Personnel = Author,
                PersonnelId = Author.Id
            });

            Project project = await _projectService.Get(ticket.ProjectId);
            
            
            Ticket createdTicket = new Ticket(){                
                Title = ticket.Title,
                Comment = ticket.Comment,
                PostDate = DateTime.UtcNow,
                ProjectId = ticket.ProjectId,
                Project = project,
                AuthorId = Author.Id,                 
                //TicketUsers = ListOfUsers,        
                Priority = ticket.Priority,
                Type = ticket.Type,
                Status = Enums.TicketStatus.Open
            };            

            await _ticketService.Add(createdTicket);
            return RedirectToAction("Index");
        }

        // GET: Issue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket currentTicket = await _context.Tickets.FindAsync(id);
            if (currentTicket == null)
            {
                return NotFound();
            }

            IList<SelectListItem> ProjectsList = new List<SelectListItem>();
            IList<SelectListItem> DeveloperList = new List<SelectListItem>();

            IList<Personnel> DeveloperPersonnel =await  _userManager.GetUsersInRoleAsync(Enums.Roles.Developer.ToString());
            IList<Project> ActiveProjects = await _context.Project.ToListAsync();

            foreach (Project project in ActiveProjects)
            {
                ProjectsList.Add(new SelectListItem(){
                    Value = project.Id.ToString(),
                    Text = project.Name,
                });
            }

            foreach (Personnel developer in DeveloperPersonnel)
            {
                DeveloperList.Add(new SelectListItem(){
                    Value = developer.Id,
                    Text=developer.UserName
                });
            }

            EditViewModel eViewModel = new EditViewModel()
            {
                Id = currentTicket.Id,
                Title = currentTicket.Title,
                Comment = currentTicket.Comment,
                AssignedDeveloperId = currentTicket.DeveloperId,
                ProjectId = currentTicket.ProjectId,
                Priority = currentTicket.Priority,
                Type = currentTicket.Type,
                Status = currentTicket.Status,
                Projects = ProjectsList,
                Developers = DeveloperList,

                
            };
            return View(eViewModel);
        }

        // POST: Issue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditViewModel editedTicket)
        {
            int ticketId = editedTicket.Id;
            if (id != ticketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Ticket updatedTicket = null;
                
                try
                {

                    try
                    {
                        updatedTicket = new Ticket(){
                            Id = editedTicket.Id,
                            Title = editedTicket.Title,
                            Comment = editedTicket.Comment,
                            ProjectId = editedTicket.ProjectId,
                            DeveloperId = editedTicket.AssignedDeveloperId,
                            Priority = editedTicket.Priority,
                            Status = editedTicket.Status,
                            Type = editedTicket.Type,                    

                        };
                    }
                    catch (System.Exception)
                    {
                        
                        throw;
                    }
                    await _ticketService.Update(updatedTicket);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticketId))
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
            return View(editedTicket);
        }

        // GET: Issue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {        
            if(id == null){
                return NotFound();
            }

            Ticket ticket = await _context.Tickets.FirstOrDefaultAsync(t=>t.Id == id);
            if(ticket == null){
                return NotFound();
            }

            DeleteViewModel dViewModel = new DeleteViewModel(){
                Id = ticket.Id,
                Title = ticket.Title
            };
            return View(dViewModel);
        }

        // POST: Issue/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Ticket ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
