using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IssueTracker.MVC.Data;
using IssueTracker.MVC.Models;
using IssueTracker.MVC.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.MVC.Repository.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        readonly ApplicationDbContext _context;
        private DbSet<Project> table = null;
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
            _context =context;
            table = _context.Set<Project>();
        }

        public void UpdateProjectUsers(Project project)
        {
             table.Attach(project);
            _context.Entry(project).Collection(u=>u.ProjectUsers).IsModified = true;
            
        }

        new public async Task<IEnumerable<Project>> GetAll()
        {
            return await table.Include(p=>p.ProjectUsers).ToListAsync();        
        }

        public Project GetByIDWithPU(int Id){
            Project curProject = table.Include(p=>p.ProjectUsers).FirstOrDefaultAsync(p=>p.Id == Id).Result;
            return curProject;
        }
    }
}
