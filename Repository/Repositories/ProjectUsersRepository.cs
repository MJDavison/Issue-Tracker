using System;
using System.Collections.Generic;
using IssueTracker.MVC.Models;
using IssueTracker.MVC.Repository.Interfaces;
using IssueTracker.MVC.ViewModels.Admin;

namespace IssueTracker.MVC.Repository.Repositories
{
    public class ProjectUsersRepository : IProjectUsersRepository
    {
        readonly IProjectRepository _projectRepo;

        public ProjectUsersRepository(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public _AllProjectUsersViewModel GetProjectUsers(int projectId){
            Project currentProject = _projectRepo.GetByID(projectId);
            
            string adminId = "";
            string projectManagerId = "";
            List<string> developerIds = new List<string>();
            List<ProjectUser> developers = new List<ProjectUser>();
            List<string> testerIds = new List<string>();
            List<ProjectUser> testers = new List<ProjectUser>();
            try
            {
                adminId = currentProject.ProjectUsers.Find(p=>p.Personnel.UserRole == "Admin").PersonnelId;
                projectManagerId = currentProject.ProjectUsers.Find(p=>p.Personnel.UserRole == "Project Manager").PersonnelId;
                developers = currentProject.ProjectUsers.FindAll(p=>p.Personnel.UserRole == "Developer");    
                foreach (ProjectUser user in developers)
                {
                    developerIds.Add(user.PersonnelId);
                }
                testers = currentProject.ProjectUsers.FindAll(p=>p.Personnel.UserRole == "Submitter");
                foreach (ProjectUser user in testers)
                {
                    testerIds.Add(user.PersonnelId);
                }            }
            catch (System.Exception)
            {
                
            }
            
            
            _AllProjectUsersViewModel _apuViewModel = new _AllProjectUsersViewModel{
                ProjectId = currentProject.Id,
                AdminId = adminId,
                ProjectManagerId = projectManagerId,
                DeveloperIds = developerIds,
                SubmitterIds = testerIds
            };

            return _apuViewModel;
        }
    }
}
