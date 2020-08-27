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

namespace IssueTracker.MVC.Controllers
{
    public class TicketController : Controller
    {

        private readonly ITicketService _ticketService;
        private readonly ITicketPersonnelService _ticketPersonnel;
        private readonly ApplicationDbContext _context;

        private readonly UserManager<Personnel> _userManager;

        [BindProperty]
        public Ticket Ticket { get; set; }
        [BindProperty]
        public List<TicketUser> TicketUsers { get; set; }

        public TicketController(ITicketService ticketService, ITicketPersonnelService ticketPersonnel, ApplicationDbContext context, UserManager<Personnel> userManager)
        {
            _ticketService = ticketService;
            _ticketPersonnel = ticketPersonnel;
            _context = context;
            _userManager = userManager;
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
            List<Ticket> openIssues = await _context.Tickets.Where(x => x.IsSolved).ToListAsync();
            List<Ticket> closedIssues = await _context.Tickets.Where(x => x.IsSolved == false).ToListAsync();
            switch (isOpen)
            {
                case true:
                    return View(openIssues);
                case false:
                    return View(closedIssues);
                
            }
            
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
        public async Task<IActionResult> Discussion(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var issueModel = await _context.Tickets
                .FirstOrDefaultAsync(m =>m.Id == id);
            if (issueModel == null)
            {
                return NotFound();
            }

            

            return View(issueModel);
        }
        // POST: Issue/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Discussion(int id, [Bind("IssueId,UserId,Title,Comment,AuthorUserName,PostDate,IsOpen")] Ticket issueModel)
        {
            int issueModelId = issueModel.Id;
            if (id != issueModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueModelExists(issueModelId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Discussion");
            }
            return View(issueModel);
        }

        // GET: Issue/Create
        public async Task<IActionResult> CreateAsync()
        {
            var personnel = await _ticketPersonnel.GetAll();
            ViewData["PersonnelList"] = new SelectList(personnel, "Id", "UserName");
            return View();
        }

        // POST: Issue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            Ticket.TicketUsers[0].Personnel = await _userManager.FindByIdAsync(Ticket.TicketUsers[0].PersonnelId);
            Ticket.AuthorId = Ticket.TicketUsers[0].PersonnelId;
            Ticket.Project = await _context.Project.FindAsync(Ticket.ProjectId);
            
            /*TODO: Currently, the TicketUser value goes straight into nothingness.*/
            if (ModelState.IsValid)
            {
                await _ticketService.Add(Ticket);
                return RedirectToAction("Details", "Projects", new { id = Ticket.Project.Id });
            }
            var personnel = await _ticketPersonnel.GetAll();
            ViewData["PersonnelList"] = new SelectList(personnel, "Id", "UserName", Ticket.AuthorId);
            return View(Ticket);
        }

        // GET: Issue/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Issue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IssueId,UserId,Title,Comment,AuthorUserName,PostDate,IsOpen")] Ticket issueModel)
        {
            int ticketId = issueModel.Id;
            if (id != ticketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueModelExists(ticketId))
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
            return View(issueModel);
        }

        // GET: Issue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueModel = await _context.Tickets
                .FirstOrDefaultAsync(m =>m.Id == id);
            if (issueModel == null)
            {
                return NotFound();
            }

            return View(issueModel);
        }

        // POST: Issue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issueModel = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(issueModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueModelExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
