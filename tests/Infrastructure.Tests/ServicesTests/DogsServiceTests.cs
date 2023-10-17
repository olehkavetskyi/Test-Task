using Application.Interfaces;
using Application.Specifications;
using Domain.Entities;
using Infrastructure.Services;
using Moq;

namespace Infrastructure.Tests.ServicesTests;


public class DogsServiceTests
{
    [Fact]
    public async Task GetAsync_ReturnsPagedDogsList()
    {
        var specParams = new SpecParams(); 
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockDogRepository = new Mock<IGenericRepository<Dog>>();
        var service = new DogsService(mockUnitOfWork.Object);

        mockUnitOfWork.Setup(u => u.Repository<Dog>()).Returns(mockDogRepository.Object);

        var dogsData = new List<Dog>
        {
            new Dog { Name = "Dog1", Color = "Brown", TailLength = 10, Weight = 20 },
            new Dog { Name = "Dog2", Color = "White", TailLength = 12, Weight = 18 },
        };

        mockDogRepository.Setup(repo => repo.ListAsync(It.IsAny<ISpecification<Dog>>()))
            .ReturnsAsync(dogsData);

        var result = await service.GetAsync(specParams);

        Assert.NotNull(result);
        Assert.Equal(dogsData.Count, result.Data.Count);
    }

    [Fact]
    public void IsDogWithTheSameNameExist_ReturnsTrueForExistingDog()
    {
        var dogToCheck = new Dog { Name = "ExistingDog" }; 
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockDogRepository = new Mock<IGenericRepository<Dog>>();
        var service = new DogsService(mockUnitOfWork.Object);

        mockUnitOfWork.Setup(u => u.Repository<Dog>()).Returns(mockDogRepository.Object);

        var existingDog = new Dog { Name = "ExistingDog", Color = "Black", TailLength = 15, Weight = 25 };
        var dogsData = new List<Dog> { existingDog };
        mockDogRepository.Setup(repo => repo.Any(It.IsAny<Func<Dog, bool>>()))
            .Returns((Func<Dog, bool> predicate) => dogsData.Any(predicate));

        var result = service.IsDogWithTheSameNameExist(dogToCheck);

        Assert.True(result);
    }

    [Fact]
    public async Task AddAsync_AddsNewDog()
    {
        var dogToAdd = new Dog { Name = "NewDog", Color = "Gray", TailLength = 14, Weight = 30 };
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockDogRepository = new Mock<IGenericRepository<Dog>>();
        var service = new DogsService(mockUnitOfWork.Object);

        mockUnitOfWork.Setup(u => u.Repository<Dog>()).Returns(mockDogRepository.Object);

        await service.AddAsync(dogToAdd);

        mockDogRepository.Verify(repo => repo.AddAsync(It.Is<Dog>(d => d.Name == "NewDog")), Times.Once);
        mockUnitOfWork.Verify(u => u.Complete(), Times.Once); 
    }

}
