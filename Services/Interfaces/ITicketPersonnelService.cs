using IssueTracker.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Services.Interfaces
{
    public interface ITicketPersonnelService
    {
        Task<TicketUser> Update(TicketUser project);

        Task<List<Personnel>> GetAll();
    }
}
