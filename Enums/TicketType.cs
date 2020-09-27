using System;
using System.ComponentModel.DataAnnotations;
namespace IssueTracker.MVC.Enums
{
    public enum TicketType
    {
        [Display(Name="Bugs/Errors")]
        BugsErrors,
        [Display(Name="Feature Requests")]
        FeatureRequests,
        [Display(Name="Other Comments")]
        OtherComments,
        [Display(Name="Training/Document Requests")]
        TrainingDocumentRequests

    }
}
