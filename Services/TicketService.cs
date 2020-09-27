using IssueTracker.MVC.Data;
using IssueTracker.MVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;

        public TicketService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> Add(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            
            return ticket;
        }

        public async Task<Ticket> Delete(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Remove(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<List<Ticket>> Get()
        {
            return await _context.Tickets.Include(r => r.Project).ToListAsync();
        }
        public async Task<List<Ticket>> GetTop(int topX)
        {
            var myItems =
                (from m in _context.Tickets
                 .Include(r => r.Project)
                 .TagWith($"This retrieves top {topX} tickets!") 
                 orderby m.Id ascending
                 select m)
                 .Take(topX);

            return (await myItems.ToListAsync());
        }

        public async Task<List<Ticket>> GetForProject(int Id)
        {
            var results = _context.Tickets
                .Where(t => t.Id == Id)
                .Include(r => r.Project);

            return await results.ToListAsync();
        }

        public async Task<Ticket> Get(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            return ticket;
        }

            
        public async Task<Ticket> Update(Ticket ticket)
        {
            using (_context){
                _context.Tickets.Attach(ticket);
                _context.Entry(ticket).Property(x=>x.Title).IsModified=true;
                _context.Entry(ticket).Property(x=>x.Comment).IsModified=true;
                _context.Entry(ticket).Property(x=>x.ProjectId).IsModified=true;
                _context.Entry(ticket).Property(x=>x.DeveloperId).IsModified=true;
                _context.Entry(ticket).Property(x=>x.Priority).IsModified=true;
                _context.Entry(ticket).Property(x=>x.Type).IsModified=true;
                _context.Entry(ticket).Property(x=>x.Status).IsModified=true;
                await _context.SaveChangesAsync();
            }
            //_context.Entry(ticket).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            return ticket;
        }
    }
}
