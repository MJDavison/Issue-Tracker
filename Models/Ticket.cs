using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IssueTracker.MVC.Enums;

namespace IssueTracker.MVC.Models
{
    public class Ticket
    {
        [Required]
        public int Id { get; set; } //Id of the project this belongs too                        
        [Required]       
        [Display(Name = "Ticket Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Ticket Description")]
        public string Comment { get; set; }
        [Required]
        public DateTime PostDate { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        
        
        public string AuthorId { get; set; }
        public string DeveloperId{get; set;}
        [DisplayName("In List")]
        //public List<TicketUser> TicketUsers { get; set; }

        public TicketPriority Priority {get; set;}
        public TicketType Type { get; set; }

        public TicketStatus Status { get; set; }

        
        
    }
}

/*
 
using System;
using System.Collections.Generic;

namespace IssueTracker.MVC.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            TicketUsers = new HashSet<TicketUsers>();
        }

        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string PersonnelId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime PostDate { get; set; }
        public bool IsSolved { get; set; }

        public Project Project { get; set; }
        public ICollection<TicketUsers> TicketUsers { get; set; }
    }
}

 
 */