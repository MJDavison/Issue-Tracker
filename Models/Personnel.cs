using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Models
{
    public sealed class Personnel
    {
        public int PersonnelId { get; set; }
        public IList<ApplicationUser> Admin = new List<ApplicationUser>();
        public IList<ApplicationUser> ProjectManager = new List<ApplicationUser>();
        public IList<ApplicationUser> Developer = new List<ApplicationUser>();
        public IList<ApplicationUser> Submitter = new List<ApplicationUser>();
        public IList<ApplicationUser> AllPersonnel = new List<ApplicationUser>();
    }
}
    

