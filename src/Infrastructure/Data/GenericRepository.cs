using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DogsHouseContext _context;

    public GenericRepository(DogsHouseContext context)
    {
        _context = context;
    }

    public bool Any(Func<T, bool> func)
    {
        return _context.Set<T>().Any(func);
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task AddAsync(T dog)
    {
        await _context.AddAsync(dog);
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    }
}
