using System.Linq.Expressions;

namespace Nero.Repository.IRepository
{
    public interface IGenralRepository<T> where T : class
    {
          IQueryable<T> GetAll();
        IEnumerable<T>? Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] expression2);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
   
        void Save();

    }
}
