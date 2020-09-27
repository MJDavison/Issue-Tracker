using System;
using System.Collections.Generic;
using IssueTracker.MVC.Models;
namespace IssueTracker.MVC.ViewModels.Projects
{
    public class ProjectTicketsViewModel
    {
        public int Id { get; set; }
        public List<Ticket> Tickets { get; set; }

    }
}
