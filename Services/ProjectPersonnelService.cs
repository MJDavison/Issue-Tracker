using IssueTracker.MVC.Data;
using IssueTracker.MVC.Data.Enums;
using IssueTracker.MVC.Models;
using IssueTracker.MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Services
{
    public class ProjectPersonnelService : IProjectPersonnelService
    {

        private readonly ApplicationDbContext _context;

        public ProjectPersonnelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Personnel>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<Personnel>> GetAdmin()
        {
            return await _context.Users.Include(a => a.UserRole == Roles.Admin.ToString()).ToListAsync();
        }

        public async Task<List<Personnel>> GetProjectManager()
        {
            return await _context.Users.Include(a => a.UserRole == Roles.ProjectManager.ToString()).ToListAsync();
        }

        public async Task<List<Personnel>> GetDevelopers()
        {
            return await _context.Users.Include(a => a.UserRole == Roles.Developer.ToString()).ToListAsync();
        }

        public async Task<List<Personnel>> GetSubmitters()
        {
            return await _context.Users.Include(a => a.UserRole == Roles.Submitter.ToString()).ToListAsync();
        }

        public Task<ProjectUser> Update(ProjectUser project)
        {
            throw new NotImplementedException();
        }
    }
}
