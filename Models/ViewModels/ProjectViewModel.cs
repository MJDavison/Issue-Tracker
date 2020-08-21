using IssueTracker.MVC.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Models.ViewModels
{
    public class ProjectViewModel
    {        
        public IEnumerable<Project> Projects = new List<Project>();
        public List<ApplicationUser> AvaliablePersonnel = new List<ApplicationUser>();
    }
}
