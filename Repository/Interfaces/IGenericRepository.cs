using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        T GetByID(object Id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object Id);
        Task Save();
    }
}
