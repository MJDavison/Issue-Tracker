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

namespace IssueTracker.MVC.Controllers
{
    public class IssueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssueController(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool IssueStatus()
        {
            string query = "";
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["status"]))
                query = HttpContext.Request.Query["status"].ToString();

            switch (query)
            {
                case "open":
                    return true;
                case "closed":
                    return false;
                default:
                    return true;
            }            
        }

        
        // GET: Issue
        public async Task<IActionResult> Index()
        {

            bool isOpen = IssueStatus();
            List<Issue> openIssues = await _context.Issues.Where(x => x.IsOpen).ToListAsync();
            List<Issue> closedIssues = await _context.Issues.Where(x => x.IsOpen == false).ToListAsync();
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

            var issueModel = await _context.Issues.FindAsync(id);
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

            var issueModel = await _context.Issues
                .FirstOrDefaultAsync(m => m.IssueId == id);
            if (issueModel == null)
            {
                return NotFound();
            }

            

            return View(issueModel);
        }
        // POST: Issue/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Discussion(int id, [Bind("IssueId,UserId,Title,Comment,AuthorUserName,PostDate,IsOpen")] Issue issueModel)
        {
            if (id != issueModel.IssueId)
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
                    if (!IssueModelExists(issueModel.IssueId))
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Issue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,IssueId,UserId,Title,Comment,AuthorUserName,PostDate,IsOpen")] Issue issueModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issueModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Projects", new { id = issueModel.ProjectId });
            }
            return View(issueModel);
        }

        // GET: Issue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueModel = await _context.Issues.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("IssueId,UserId,Title,Comment,AuthorUserName,PostDate,IsOpen")] Issue issueModel)
        {
            if (id != issueModel.IssueId)
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
                    if (!IssueModelExists(issueModel.IssueId))
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

            var issueModel = await _context.Issues
                .FirstOrDefaultAsync(m => m.IssueId == id);
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
            var issueModel = await _context.Issues.FindAsync(id);
            _context.Issues.Remove(issueModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueModelExists(int id)
        {
            return _context.Issues.Any(e => e.IssueId == id);
        }
    }
}
