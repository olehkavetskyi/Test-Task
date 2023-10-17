using Application.Interfaces;
using System.Linq.Expressions;

namespace Application.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{

    public Expression<Func<T, object>>? Attribute { get; set; } 

    public Expression<Func<T, object>>? OrderBy { get; private set; } 

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddAttribute(Expression<Func<T, object>> attribute)
    {
        Attribute = attribute;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}