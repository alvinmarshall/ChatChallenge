using System.Linq.Expressions;
using Domain.Specification;

namespace Domain.Repository;

public interface IBaseRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetAll(IBaseSpecification<TEntity> specification);
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression, IBaseSpecification<TEntity> specification);

    Task<TEntity> SaveAsync(TEntity entity);
    
    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity?> GetByIdAsync(Guid id);

    Task<TEntity?> GetByIdAsync(Guid id, IBaseSpecification<TEntity> specification);

    Task RemoveAsync(TEntity entity);
}