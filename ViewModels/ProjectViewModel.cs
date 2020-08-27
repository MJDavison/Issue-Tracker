using IssueTracker.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.ViewModels
{
    public class ProjectViewModel
    {
        public ProjectViewModel()
        {
        }

        public ProjectViewModel(Project project)
        {
            Project = project;
        }
        public int Id { get; set; }
        public Project Project { get; set; }

        public IEnumerable<Personnel> Personnels { get; set; }
    }
}
