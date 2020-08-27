using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Models
{
    public class TicketUser
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public string PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
    }
}
