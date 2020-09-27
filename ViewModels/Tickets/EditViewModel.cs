using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using IssueTracker.MVC.Models;
namespace IssueTracker.MVC.ViewModels.Tickets
{
    using IssueTracker.MVC.Enums;
    public class EditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }

        public string AssignedDeveloperId { get; set; }

        public int ProjectId { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketType Type { get; set; }
        public TicketStatus Status { get; set; }

        public IList<SelectListItem> Projects { get; set; }        
        public IList<SelectListItem> Developers { get; set; }  
    }
}
