using FligthPlanner.Core.Models;
using FligthPlanner.Core.Services;
using FligthPlanner.Data;

namespace FligthPlanner.Services
{
    public class EntityService<T> : DbService, IEntityService<T> where T : Entity
    {
        public EntityService(IFlightPlannerDbContext context) : base(context) { }
        public T Create(T entity) => Create<T>(entity);
        public void Update(T entity) => Update<T>(entity);
        public void DeleteAll() => DeleteAll<T>();
        public void Delete(T entity) => Delete<T>(entity);
        public IEnumerable<T> GetAll() => GetAll<T>();
        public T? GetById(int id) => GetById<T>(id);
        public void Clear(int id) => Clear(id);
    }
}
