using Domain.Entities;

namespace Application.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task AddAsync(T dog);
    bool Any(Func<T, bool> func);
    Task<int> CountAsync(ISpecification<T> spec);
    IQueryable<T> GetAll();
}
