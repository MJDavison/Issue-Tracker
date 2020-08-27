using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Models
{
    public class ProjectUser
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public string PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
    }
}
