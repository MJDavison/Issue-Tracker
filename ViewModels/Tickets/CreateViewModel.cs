using System;
using IssueTracker.MVC.Enums;
namespace IssueTracker.MVC.ViewModels.Tickets
{
    public class CreateViewModel
    {      
        public int ProjectId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        
        public TicketPriority Priority {get; set;}
        public TicketType Type { get; set; }

    }
}
