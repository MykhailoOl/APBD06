using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Tutorial6.Models;
using Tutorial6.Models.DTOs;
using Tutorial6.Repositories;

namespace Tutorial6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private AnimalRepository _animalRepository;
    public AnimalsController(IConfiguration configuration,AnimalRepository animalRepository)
    {
        _configuration = configuration;
        _animalRepository = animalRepository;
    }
    [HttpGet]
    public IActionResult GetAnimals([FromQuery] string orderBy = "name")
    {
        var animals = _animalRepository.getListOfAnimals(_configuration,orderBy);
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal animal)
    {
        _animalRepository.AddAnimal(_configuration,animal);
        return Created("", null);
    }
    
    [HttpPut]
    public IActionResult UpdateAnimal(Animal animal)
    {
        _animalRepository.UpdateAnimal(_configuration,animal);
        return Ok(animal);
    }

    [HttpDelete]
    public IActionResult DeleteAnimalById(int id)
    {
        _animalRepository.DeleteAnimal(_configuration,id);
        return Ok();
    }
}