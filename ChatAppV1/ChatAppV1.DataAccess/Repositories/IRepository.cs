using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatAppV1.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        T GetById(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
