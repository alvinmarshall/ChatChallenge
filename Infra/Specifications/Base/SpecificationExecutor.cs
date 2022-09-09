using Domain.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infra.Specifications.Base;

public static class SpecificationExecutor<TEntity> where TEntity : class
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> query, IBaseSpecification<TEntity>? specification)
    {
        if (specification is null) return query;
        if (specification.FilterCondition is not null)
        {
            query = query.Where(specification.FilterCondition);
        }

        query = specification.Includes
            .Aggregate(query, (entities, expression) => entities.Include(expression));

        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }

        if (specification.OrderByDesc is not null)
        {
            query = query.OrderByDescending(specification.OrderByDesc);
        }

        if (specification.GroupBy is not null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(entities => entities);
        }

        return query;
    }
}