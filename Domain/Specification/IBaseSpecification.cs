using System.Linq.Expressions;

namespace Domain.Specification;

public interface IBaseSpecification<TEntity>
{
    Expression<Func<TEntity, bool>>? FilterCondition { get; }

    Expression<Func<TEntity, object>>? OrderBy { get; }

    Expression<Func<TEntity, object>>? OrderByDesc { get; }

    Expression<Func<TEntity, object>>? GroupBy { get; }

    List<Expression<Func<TEntity, object>>> Includes { get; }
}