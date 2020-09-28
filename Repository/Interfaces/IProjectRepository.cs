using System;
using IssueTracker.MVC.Models;

namespace IssueTracker.MVC.Repository.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        void UpdateProjectUsers(Project project);
        Project GetByIDWithPU(int Id);
        
    }
}
