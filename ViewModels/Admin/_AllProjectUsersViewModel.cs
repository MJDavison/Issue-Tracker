using System;
using System.Collections.Generic;

namespace IssueTracker.MVC.ViewModels.Admin
{
    public class _AllProjectUsersViewModel
    {
        public int ProjectId { get; set; }
        public string AdminId { get; set; }
        public string ProjectManagerId { get; set; }

        public List<string> DeveloperIds { get; set; }
        public List<string> SubmitterIds { get; set; }
    }
}
