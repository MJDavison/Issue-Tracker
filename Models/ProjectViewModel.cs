using IssueTracker.MVC.Data;
using IssueTracker.MVC.Services;
using IssueTracker.MVC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Models
{
    public class ProjectViewModel
    {
        public int Id;
        public string Name;
        public string Description;

        public List<Personnel> Personnels { get; set; }
    }
}
