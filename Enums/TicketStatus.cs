using System;
using System.ComponentModel.DataAnnotations;
namespace IssueTracker.MVC.Enums
{
    public enum TicketStatus
    {
        Open,
        [Display(Name="In Progress")]
        InProgress,
        Resolved,
        [Display(Name="Additional Info Required")]
        AdditionalInfoRequired
    }
}
