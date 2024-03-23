
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        T GetById(Guid id);
        T GetByNoTrackId(Guid id);
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> exp);
        List<T> Where(Expression<Func<T, bool>> exp);
        IQueryable<T> WhereQueryable(Expression<Func<T, bool>> expression);
        bool Any(Expression<Func<T, bool>> exp);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        T Default(Expression<Func<T, bool>> exp);
        bool AddRange(List<T> model);
        void ToggleStatus(Guid Id);
        void Delete(Guid Id);
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> exp);
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> exp);
        Task<T> AddAsync(T entity);


    }
}
