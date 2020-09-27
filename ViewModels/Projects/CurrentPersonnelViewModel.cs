using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IssueTracker.MVC.Models;

namespace IssueTracker.MVC.ViewModels.Projects
{
    public class CurrentPersonnelViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }

         public List<ProjectUser> ProjectUsers { get; set; }
    }
}
