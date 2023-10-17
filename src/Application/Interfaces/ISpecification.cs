using System.Linq.Expressions;

namespace Application.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, object>>? Attribute { get; set; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}