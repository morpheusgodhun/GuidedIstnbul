using Core.Entities;
using Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly Context _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public async Task<T> AddAsync(T entity)
        {
            var entityEntry = await _dbSet.AddAsync(entity);
            return entityEntry.Entity;
        }

        public bool AddRange(List<T> model)
        {
            _dbSet.AddRange(model);
            return true;
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            return _dbSet.Any(exp);
        }

        public T Default(Expression<Func<T, bool>> exp)
        {
            return _dbSet.FirstOrDefault(exp);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public List<T> GetAll(Expression<Func<T, bool>> exp)
        {
            return _dbSet.Where(exp).ToList();
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> exp)
        {
            return await _dbSet.Where(exp).ToListAsync();
        }

        public virtual T GetById(Guid id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
        public T GetByNoTrackId(Guid id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void ToggleStatus(Guid Id)
        {
            T entity = _dbSet.Find(Id);
            entity.Status = !entity.Status;
            _dbSet.Update(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public List<T> Where(Expression<Func<T, bool>> exp)
        {
            return _dbSet.Where(exp).ToList();
        }
        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public void Delete(Guid Id)
        {
            T entity = _dbSet.Find(Id);
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }

        public IQueryable<T> WhereQueryable(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).AsQueryable();
        }
    }
}
