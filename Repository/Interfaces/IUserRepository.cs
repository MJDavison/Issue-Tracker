using System;
using IssueTracker.MVC.Models;

namespace IssueTracker.MVC.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<Personnel>
    {
        void UpdateUserRole(Personnel user);
    }
}
