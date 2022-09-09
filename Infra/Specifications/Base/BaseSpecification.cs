using System.Linq.Expressions;
using Domain.Specification;

namespace Infra.Specifications.Base;

public class BaseSpecification<TEntity> : IBaseSpecification<TEntity> where TEntity : class
{
    public Expression<Func<TEntity, bool>> FilterCondition { get; private set; }
    public Expression<Func<TEntity, object>> OrderBy { get; private set; }
    public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }
    public Expression<Func<TEntity, object>> GroupBy { get; private set; }
    public List<Expression<Func<TEntity, object>>> Includes { get; } = new();

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
    
    protected void ApplyOrderBy(Expression<Func<TEntity, object>> orderByAscExpression)
    {
        OrderBy = orderByAscExpression;
    }
    
    protected void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDesExpression)
    {
        OrderByDesc = orderByDesExpression;
    }

    protected void SetFilterCondition(Expression<Func<TEntity, bool>> filterExpression)
    {
        FilterCondition = filterExpression;
    }

    protected void ApplyGroupBy(Expression<Func<TEntity, object>> groupByExpression)
    {
        GroupBy = groupByExpression;
    }
}