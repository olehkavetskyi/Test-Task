using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Controllers;

namespace Web.Tests.Controllers.Tests;

public class DogsControllerTests
{
    [Fact]
    public async Task AddAsync_ValidData_ReturnsOkResult()
    {
        var mockDogsService = new Mock<IDogsService>();
        var controller = new DogsController(mockDogsService.Object);
        var dogToAdd = new Dog { Name = "Rover", Color = "Brown", TailLength = 12, Weight = 25 };

        var result = await controller.AddAsync(dogToAdd);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("The dog was added successfully", okResult.Value);
    }

    [Fact]
    public async Task AddAsync_DuplicateName_ReturnsBadRequest()
    {
        var mockDogsService = new Mock<IDogsService>();
        mockDogsService.Setup(service => service.IsDogWithTheSameNameExist(It.IsAny<Dog>())).Returns(true);
        var controller = new DogsController(mockDogsService.Object);
        var dogToAdd = new Dog { Name = "Rover", Color = "Black", TailLength = 14, Weight = 30 };

        var result = await controller.AddAsync(dogToAdd);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("A dog with the same name already exists", badRequestResult.Value);
    }

    [Fact]
    public async Task AddAsync_InvalidName_ReturnsBadRequestWithErrors()
    {
        var mockDogsService = new Mock<IDogsService>();
        var controller = new DogsController(mockDogsService.Object);
        controller.ModelState.AddModelError("Name", "Name is required");
        var dogToAdd = new Dog { Name = null!, Color = "Gray", TailLength = 15, Weight = 22 };

        var result = await controller.AddAsync(dogToAdd);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errors = Assert.IsType<List<string>>(badRequestResult.Value);
        Assert.Contains("Name is required", errors);
    }


    [Fact]
    public async Task AddAsync_NegativeTailLength_ReturnsBadRequestWithErrors()
    {
        var mockDogsService = new Mock<IDogsService>();
        var controller = new DogsController(mockDogsService.Object);
        controller.ModelState.AddModelError("TailLength", "tail_length cannot be negative"); 
        var dogToAdd = new Dog { Name = "Fido", Color = "Gray", TailLength = -5, Weight = 22 };

        var result = await controller.AddAsync(dogToAdd);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errors = Assert.IsType<List<string>>(badRequestResult.Value);
        Assert.Contains("tail_length cannot be negative", errors);
    }

}