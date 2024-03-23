using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseEntity
    {
        protected readonly IGenericRepository<T> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        public GenericService(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public virtual T Add(T entity)
        {
            _repository.Add(entity);
            _unitOfWork.saveChanges();
            return entity;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            _unitOfWork.saveChanges();
            return entity;
        }
        public bool Any(Expression<Func<T, bool>> exp)
        {
            return _repository.Any(exp);
        }
        public List<T> GetAll()
        {
            return _repository.GetAll();
        }
        public async Task<List<T>> GetAllAsync() => await _repository.GetAllAsync();
        public List<T> GetAll(Expression<Func<T, bool>> exp)
        {
            return _repository.GetAll(exp);
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> exp) => await _repository.GetAllAsync(exp);
        public T GetById(Guid id) => _repository.GetById(id);
        public async Task<T> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
        public void Remove(T entity)
        {
            _repository.Remove(entity);
            _unitOfWork.saveChanges();
        }
        public virtual void Update(T entity)
        {
            _repository.Update(entity);
            _unitOfWork.saveChanges();
        }
        public List<T> Where(Expression<Func<T, bool>> exp) => _repository.Where(exp);
        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> expression) => await _repository.GetWhereAsync(expression);
        public List<T> AddRange(List<T> entity)
        {
            _repository.AddRange(entity);
            _unitOfWork.saveChanges();

            return entity;
        }
        public T GetByNoTrackId(Guid id)
        {
            return _repository.GetByNoTrackId(id);
        }
        public void ToggleStatus(Guid Id)
        {
            _repository.ToggleStatus(Id);
            _unitOfWork.saveChanges();
        }
        public virtual void Delete(Guid Id)
        {
            _repository.Delete(Id);
            _unitOfWork.saveChanges();
        }
    }
}
