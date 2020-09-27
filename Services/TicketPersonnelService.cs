using IssueTracker.MVC.Data;
using IssueTracker.MVC.Models;
using IssueTracker.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.MVC.Data.Enums;

namespace IssueTracker.MVC.Services
{
    public class TicketPersonnelService : ITicketPersonnelService
    {
        private readonly ApplicationDbContext _context;
        
        public TicketPersonnelService(ApplicationDbContext context)
        {
            _context = context;
        }
                    
        public async Task<Ticket> Update(Ticket ticket)
        {
            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ticket;
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
            return await _context.Users.Include(a=>a.UserRole==Roles.ProjectManager.ToString()).ToListAsync();
        }

        public async Task<List<Personnel>> GetDevelopers()
        {
            return await _context.Users.Include(a => a.UserRole == Roles.Developer.ToString()).ToListAsync();
        }

        public async Task<List<Personnel>> GetSubmitters()
        {
            return await _context.Users.Include(a => a.UserRole == Roles.Submitter.ToString()).ToListAsync();
        }




        public Task<TicketUser> Update(TicketUser project)
        {
            throw new NotImplementedException();
        }

        
    }
}
