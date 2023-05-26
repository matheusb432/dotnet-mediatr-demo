using Microsoft.EntityFrameworkCore;
using DemoApp.Domain.Models;
using System.Linq.Expressions;

namespace DemoApp.Infra.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> Query();

        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(long id);

        Task<T?> GetByIdMinimalAsync(long id);

        Task<bool> ExistsAsync(long id);

        Task ModifyAsync(T entity, bool save = true);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> GetManyByIdsAsync(List<int> ids);

        Task<T> InsertAsync(T entity, bool save = true);

        Task<IEnumerable<T>> InsertAllAsync(IEnumerable<T> entities, bool save = true);

        Task<T> UpdateAsync(T entity, bool save = true);

        Task DeleteAsync(T entity, bool save = true);

        Task DeleteAllAsync(IEnumerable<T> entities, bool save = true);

        Task SaveChangesAsync();
    }

    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly DemoAppContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly int _userId;
        protected readonly bool _isAdmin;

        protected Repository(DemoAppContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual IQueryable<T> Query() => _dbSet.AsQueryable();

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await Query().ToListAsync();

        public virtual async Task<T?> GetByIdAsync(long id) =>
            await Query().FirstOrDefaultAsync(e => e.Id == id);

        public async Task<T?> GetByIdMinimalAsync(long id) =>
            await Query()
                .AsNoTracking()
                .Select(x => new T { Id = x.Id })
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) =>
            await Query().AnyAsync(predicate);

        public async Task<bool> ExistsAsync(long id) => await Query().AnyAsync(x => x.Id == id);

        public virtual async Task<List<T>> GetManyByIdsAsync(List<int> ids) =>
            await Query().Where(x => ids.Contains(x.Id)).ToListAsync();

        public async Task<T> InsertAsync(T entity, bool save = true)
        {
            await _dbSet.AddAsync(entity);

            if (save)
                await SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<T>> InsertAllAsync(IEnumerable<T> entities, bool save = true)
        {
            await _dbSet.AddRangeAsync(entities);

            if (save)
                await SaveChangesAsync();

            return entities;
        }

        public async Task<T> UpdateAsync(T entity, bool save = true)
        {
            _dbSet.Update(entity);

            if (save)
                await SaveChangesAsync();

            return entity;
        }

        public async Task ModifyAsync(T entity, bool save = true)
        {
            _dbSet.Entry(entity).State = EntityState.Modified;

            if (save)
                await SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity, bool save = true)
        {
            _dbSet.Remove(entity);

            if (save)
                await SaveChangesAsync();
        }

        public async Task DeleteAllAsync(IEnumerable<T> entities, bool save = true)
        {
            _dbSet.RemoveRange(entities);

            if (save)
                await SaveChangesAsync();
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
