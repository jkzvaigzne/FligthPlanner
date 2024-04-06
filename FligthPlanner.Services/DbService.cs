using Microsoft.EntityFrameworkCore;
using FligthPlanner.Core.Services;
using FligthPlanner.Data;
using FligthPlanner.Core.Models;

namespace FligthPlanner.Services
{
    public class DbService : IDbService
    {
        protected readonly IFlightPlannerDbContext _context;
        public DbService(IFlightPlannerDbContext context) 
        {
            _context = context;
        } 

        public T Create<T>(T entity) where T : Entity
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteAll<T>() where T : Entity
        {
            _context.Set<T>().RemoveRange(_context.Set<T>());
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        public T? GetById<T>(int id) where T : Entity => _context.Set<T>().SingleOrDefault(entity => entity.Id == id);
        public IEnumerable<T> GetAll<T>() where T : Entity => _context.Set<T>().ToList();
    }
}
