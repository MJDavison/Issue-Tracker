using IssueTracker.MVC.Data;
using IssueTracker.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Services
{
    public interface ITicketService
    {
        Task<List<Ticket>> Get();
        Task<List<Ticket>> GetForProject(int Id);
        Task<List<Ticket>> GetTop(int topX);
        Task<Ticket> Get(int id);
        Task<Ticket> Add(Ticket ticket);
        Task<Ticket> Update(Ticket ticket);
        Task<Ticket> Delete(int id);


    }
}
