using Microsoft.EntityFrameworkCore;
using Mvc_Project.Utility;
using System;
using System.Linq.Expressions;

namespace Mvc_Project.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ProjectDbContext _context;
        internal DbSet<T> dbSet;
        //DbSet=_context.KitapTuru;

        public Repository(ProjectDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
            _context.Kitaplar.Include(p => p.KitapTuru).Include(k => k.KitapTuruId);
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> predicate, string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            query=query.Where(predicate);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProps=null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

    }
}
