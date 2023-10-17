using Application.Helpers;
using Application.Specifications;
using Domain.Entities;

namespace Application.Interfaces;

public interface IDogsService
{
    Task AddAsync(Dog dog);
    Task<Pagination<Dog>> GetAsync(SpecParams spec);
    bool IsDogWithTheSameNameExist(Dog dog);
}
