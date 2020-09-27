using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using IssueTracker.MVC.Models;

namespace IssueTracker.MVC.ViewModels.Admin
{
    public class ManageRolesViewModel
    {        
        public List<SelectListItem> Personnel { get; set; }
        public List<string> PersonnelIdsToChange {get; set;}
        public int roleId { get; set; }
    }
}
