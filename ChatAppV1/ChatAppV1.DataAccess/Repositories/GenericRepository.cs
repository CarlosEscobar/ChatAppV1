using ChatAppV1.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatAppV1.DataAccess.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly ChatAppContext dbContext;
        private readonly DbSet<T> dbSet;

        public GenericRepository(ChatAppContext context)
        {
            this.dbContext = context;
            this.dbSet = this.dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await this.dbSet.ToListAsync();
        }

        public T GetById(int id)
        {
            return this.dbSet.Find(id);
        }

        public void Create(T entity)
        {
            this.dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            this.dbSet.Attach(entity);
            this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            this.dbSet.Remove(entity);
        }
    }
}
