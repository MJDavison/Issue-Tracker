using IssueTracker.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Services
{
    public interface IProjectService
    {

        Task<List<Project>> Get();        
        Task<List<Project>> GetTop(int topX);
        Task<Project> Get(int? id);
        Task<Project> Add(Project ticket);
        Task<Project> Update(Project ticket);
        Task<Project> Delete(int id);
    }
}
