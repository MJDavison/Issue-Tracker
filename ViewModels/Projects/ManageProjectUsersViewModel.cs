using System;
using System.Collections.Generic;
using IssueTracker.MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace IssueTracker.MVC.ViewModels.Projects
{
    public class ManageProjectUsersViewModel
    {
        public string Name { get; set; }

        public int Id { get; set; }
        
        public List<SelectListItem> Personnels {get; set;}
        public List<ProjectUser> ProjectUsers {get; set;}
    }
}
