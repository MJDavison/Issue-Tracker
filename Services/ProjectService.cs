using IssueTracker.MVC.Data;
using IssueTracker.MVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Services
{
    public class ProjectService : IProjectService
    {

        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Project> Add(Project project)
        {
            _context.Project.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> Delete(int id)
        {
            var project = await _context.Project.FindAsync(id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<List<Project>> Get()
        {
            return await _context.Project.ToListAsync();
        }

        public async Task<Project> Get(int? id)
        {
            var project = await _context.Project
                .Include(t => t.ProjectUsers)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            return project;
        }

        public Task<List<Project>> GetTop(int topX)
        {
            throw new NotImplementedException();
        }

        public async Task<Project> Update(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return project;
        }
    }
}
