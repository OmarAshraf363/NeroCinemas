using Microsoft.EntityFrameworkCore;
using Nero.Data;
using System.Linq.Expressions;

namespace Nero.Repository.IRepository
{
    public class GenralRepository<T> : IGenralRepository<T> where T : class
    {
        protected readonly AppDbContext context;
        protected readonly DbSet<T> dbSet;

        public GenralRepository(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        public virtual IQueryable<T> GetAll()
        {
            return dbSet.AsQueryable();
        }
       

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = dbSet.Find(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                Save();
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
      

        public IEnumerable<T>? Get(Expression<Func<T, bool>> expression , params Expression<Func<T, object>> []expression2)
        {
            IQueryable<T> query = dbSet.Where(expression);
            if (expression2 != null&&expression2.Length>0)
            {
                for (int i=0;i<expression2.Length;i++)
                {
                  query = query.Include(expression2[i]);

                }
                return query;
            }
            else
            {
                return dbSet.Where(expression);
            }
           
        }

    }
}
