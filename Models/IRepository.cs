using System.Linq.Expressions;

namespace Mvc_Project.Models
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProps = null);
        T Get(Expression<Func<T, bool>> predicate, string? includeProps = null);
        void Add(T entity);
        void Delete(T entity);
    }
}
