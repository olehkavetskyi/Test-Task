using Application.Helpers;
using Application.Interfaces;
using Application.Specifications;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
public class DogsController : ControllerBase
{
    private readonly IDogsService _dogsService;

    public DogsController(IDogsService dogsService)
    {
        _dogsService = dogsService;
    }

    [HttpGet("ping")]
    public ActionResult Ping()
    {
        return Ok("Dogshouseservice.Version1.0.1");
    }

    [HttpPost("dog")]
    public async Task<ActionResult> AddAsync(Dog dog)
    {
        if (_dogsService.IsDogWithTheSameNameExist(dog)) 
        {
            return BadRequest("A dog with the same name already exists");
        }
        else if (ModelState.IsValid)
        {
            await _dogsService.AddAsync(dog);

            return Ok("The dog was added successfully");
        }
        else
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage)
                 .ToList();

            return BadRequest(errors);
        }
    }

    [HttpGet("dogs")]
    public async Task<ActionResult<Pagination<Dog>>> GetAsync([FromQuery]SpecParams spec)
    {
        var result = await _dogsService.GetAsync(spec);

        return result;
    }
}
