using System;
using IssueTracker.MVC.ViewModels.Admin;

namespace IssueTracker.MVC.Repository.Interfaces
{
    public interface IProjectUsersRepository
    {
        _AllProjectUsersViewModel GetProjectUsers(int projectId);
    }
}
