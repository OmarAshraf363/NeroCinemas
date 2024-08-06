using Microsoft.EntityFrameworkCore;
using Nero.Data;

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
        public virtual T? GetById(int id)
        {
            return dbSet.Find(id);
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
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
