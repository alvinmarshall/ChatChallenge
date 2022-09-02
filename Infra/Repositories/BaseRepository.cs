using System.Linq.Expressions;
using Domain.Repository;
using Infra.Context;

namespace Infra.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
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

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().Where(expression);
    }

    public async Task<TEntity> SaveAsync(TEntity entity)
    {
        var entity1 = Context.Set<TEntity>().Add(entity).Entity;
        await Context.SaveChangesAsync();
        return entity1;
    }

    public ValueTask<TEntity?> GetByIdAsync(Guid id)
    {
        return Context.Set<TEntity>().FindAsync(id);
    }

    public async Task RemoveAsync(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync();
    }
}