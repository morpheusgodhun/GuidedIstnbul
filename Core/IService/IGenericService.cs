
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface IGenericService<T> where T : BaseEntity
    {
        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);
        T GetByNoTrackId(Guid id);
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
        List<T> GetAll(Expression<Func<T, bool>> exp);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> exp);
        List<T> Where(Expression<Func<T, bool>> exp);
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> expression);
        bool Any(Expression<Func<T, bool>> exp);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        List<T> AddRange(List<T> entity);
        void ToggleStatus(Guid Id);
        void Delete(Guid Id);

        //bool AnyAsync(Expression<Func<T, bool>> expression);
        //Task<int> CountAsync(Expression<Func<T, bool>> expression);
    }
}
