using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IssueTracker.MVC.ViewModels.Admin
{
    public class ManageProjectUsersViewModel
    {
    
        //Output
        public List<SelectListItem> Projects { get; set;}
        public List<SelectListItem> Admins { get; set;}
        public List<SelectListItem> Developers { get; set;}
        public List<SelectListItem> ProjectManagers { get; set;}
        public List<SelectListItem> Submitters { get; set;}


        //Input
        public List<int> ProjectIds { get; set; }
        public string AdminId { get; set; }
        public string ProjectManagerId { get; set; }

        public List<string> DeveloperIds { get; set; }
        public List<string> SubmitterIds { get; set; }

    }
}
