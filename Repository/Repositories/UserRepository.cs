using System;
using IssueTracker.MVC.Data;
using IssueTracker.MVC.Models;
using IssueTracker.MVC.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.MVC.Repository.Repositories
{
    public class UserRepository : GenericRepository<Personnel>, IUserRepository
    {
        readonly ApplicationDbContext _context;
        private DbSet<Personnel> table = null;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            table = _context.Set<Personnel>();
        }

        public void UpdateUserRole(Personnel user)
        {
            table.Attach(user);
            _context.Entry(user).Property(u=>u.UserRole).IsModified = true;
        }
    }
}
