using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Display(Name = "Project Description")]
        public string ProjectDescription { get; set; }

        public Personnel Personnel = new Personnel();
        public List<Issue> Issues = new List<Issue>();        
    }



}
