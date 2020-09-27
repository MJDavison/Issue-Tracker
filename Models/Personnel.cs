using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Models
{
    public class Personnel : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; } 

        public List<ProjectUser> ProjectUsers { get; set; }
        //public List<TicketUser> TicketUsers { get; set; }

    }
}
