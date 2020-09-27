using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IssueTracker.MVC.Data;
using IssueTracker.MVC.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.MVC.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
         readonly ApplicationDbContext _context;
         private DbSet<T> table = null;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;         
            table = _context.Set<T>();   
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();        
        }

        public T GetByID(object Id)
        {
            return table.Find(Id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object Id)
        {
            T existing = table.Find(Id);
            table.Remove(existing);
        }
        public async void Save()
        {
           await _context.SaveChangesAsync();
        }

        
    }
}
