using System;

namespace IssueTracker.MVC.ViewModels.Tickets
{
    using IssueTracker.MVC.Enums;
    public class DetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }

        public TicketPriority Priority { get; set; }
        public TicketType Type { get; set; }

        public TicketStatus Status { get; set; }

        public string Project { get; set; }
    }
}
