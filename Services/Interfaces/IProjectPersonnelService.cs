using IssueTracker.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Services.Interfaces
{
    public interface IProjectPersonnelService
    {
        Task<ProjectUser> Update(ProjectUser project);

        Task<List<Personnel>> GetAll();
    }
}
