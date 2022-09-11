using System.Linq.Expressions;
using Domain.Repository;
using Domain.Specification;
using Infra.Context;
using Infra.Entities;
using Infra.Specifications.Base;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ChatAppContext Context;

    protected BaseRepository(ChatAppContext context)
    {
        Context = context;
    }


    public IEnumerable<TEntity> GetAll()
    {
        return Context.Set<TEntity>().ToList();
    }

    public IEnumerable<TEntity> GetAll(IBaseSpecification<TEntity>? specification)
    {
        return SpecificationExecutor<TEntity>
            .GetQuery(Context.Set<TEntity>().AsNoTracking().AsQueryable(), specification)
            .ToList();
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().AsNoTracking().Where(expression);
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression,
        IBaseSpecification<TEntity> specification)
    {
        return SpecificationExecutor<TEntity>.GetQuery(
            Context.Set<TEntity>()
                .AsNoTracking()
                .Where(expression)
                .AsQueryable(), specification);
    }

    public async Task<TEntity> SaveAsync(TEntity entity)
    {
        var entity1 = Context.Set<TEntity>().Add(entity).Entity;
        await Context.SaveChangesAsync();
        return entity1;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entity1 = Context.Set<TEntity>().Attach(entity).Entity;
        await Context.SaveChangesAsync();
        return entity1;
    }

    public Task<TEntity?> GetByIdAsync(Guid id)
    {
        return Context.Set<TEntity>().AsNoTracking().Where(entity => entity.Id == id).FirstOrDefaultAsync();
    }

    public Task<TEntity?> GetByIdAsync(Guid id, IBaseSpecification<TEntity> specification)
    {
        return SpecificationExecutor<TEntity>.GetQuery(
                Context.Set<TEntity>()
                    .AsNoTracking()
                    .Where(entity => entity.Id == id)
                    .AsQueryable(), specification)
            .FirstOrDefaultAsync();
    }

    public async Task RemoveAsync(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync();
    }
}