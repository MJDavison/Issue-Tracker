using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Models.ViewModels
{
    public class MyUserRoleViewModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public List<ApplicationUser> UserList { get; set; }
    }
}
