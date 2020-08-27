using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IssueTracker.MVC.Models
{
    public class Project
    {
        [Required]
        public int Id { get; set; }        

        [Required]
        [Display(Name = "Project Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Project Description")]
        public string Description { get; set; }

        public List<Ticket> Tickets { get; set; }
        public List<ProjectUser> ProjectUsers { get; set; }
    }
}
/*
 * using System;
using System.Collections.Generic;

namespace IssueTracker.MVC.Models
{
    public partial class Project
    {

        
        public Project()
        {
            ProjectUsers = new HashSet<ProjectUsers>();
            Tickets = new HashSet<Ticket>();
        }

        public string Id { get; set; }
        public string PersonnelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ProjectUsers> ProjectUsers { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
 

 */
