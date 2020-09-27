using System;

namespace IssueTracker.MVC.ViewModels.Tickets
{
    using IssueTracker.MVC.Enums;
    public class EditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
    }
}
