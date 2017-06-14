using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace Cibertec.Repositories
{
    public class RepositoryEF<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        public RepositoryEF(DbContext context)
        {
            _context = context;
        }

        public bool Delete(T entity)
        {
            _context.Remove(entity);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public T GetEntityById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        

        public int Insert(T entity)
        {
            _context.Add(entity);
            return _context.SaveChanges();
        }

        public bool Update(T entity)
        {
            _context.Update(entity);
            return _context.SaveChanges() > 0;
        }
    }
}
