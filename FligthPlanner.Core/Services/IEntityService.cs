using FligthPlanner.Core.Models;

namespace FligthPlanner.Core.Services
{
    public interface IEntityService<T> where T : Entity
    {
        void Clear(int id);
        T Create(T entity);
        void Delete(T entity);
        void DeleteAll();
        IEnumerable<T> GetAll();
        T? GetById(int id);
        public void Update(T entity);
    }
}
