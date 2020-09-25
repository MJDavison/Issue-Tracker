using IssueTracker.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.ViewModels
{
    public class TicketViewModel
    {
       
        public int Id { get; set; }
       
        public string Title { get; set; }
       
        public string Comment { get; set; }
       
        public DateTime PostDate { get; set; }
       
        public int ProjectId { get; set; }
    }
}
