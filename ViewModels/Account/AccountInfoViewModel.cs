using IssueTracker.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.ViewModels.Account
{
    public class AccountInfoViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }

        public bool EmailConfirmed { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }        
        public string PhoneNumber { get; set; }
    }
}
