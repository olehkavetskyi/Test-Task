using Application.Helpers;
using Application.Interfaces;
using Application.Specifications;
using Domain.Entities;

namespace Infrastructure.Services;

public class DogsService : IDogsService
{
    private readonly IUnitOfWork _unitOfWork;

    public DogsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Pagination<Dog>> GetAsync(SpecParams specParams)
    {
        var spec = new GogsWithSortingAndPaginationSpecification(specParams);

        var dogs = await _unitOfWork.Repository<Dog>().ListAsync(spec);

        var totalItems = await _unitOfWork.Repository<Dog>().CountAsync(spec);

        if (!specParams.PageSize.HasValue)
            specParams.PageSize = totalItems;

        return new Pagination<Dog>(specParams.PageNumber, (int)specParams.PageSize!, totalItems, dogs);
    }

    public bool IsDogWithTheSameNameExist(Dog dog)
    {
        return _unitOfWork.Repository<Dog>().Any(x => x.Name == dog.Name);
    }

    public async Task AddAsync(Dog dog)
    {
        await _unitOfWork.Repository<Dog>().AddAsync(dog);
        await _unitOfWork.Complete();
    }
}
