using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Tutorial6.Models;

namespace Tutorial6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpGet]
    public IActionResult GetAnimals()
    {
        // Open connection
        SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        // Create command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM ANIMAL;";
        // Execute command
        var reader = command.ExecuteReader();
        var animals = new List<Animal>();
        int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
        int NameOrdinal = reader.GetOrdinal("Name");
        int DescriptionOrdinal = reader.GetOrdinal("Description");
        int CategoryOrdinal = reader.GetOrdinal("Category");
        int AreaOrdinal = reader.GetOrdinal("Area");
        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOrdinal),
                Name = reader.GetString(NameOrdinal),
                Description = reader.GetString(DescriptionOrdinal),
                Category = reader.GetString(CategoryOrdinal),
                Area = reader.GetString(AreaOrdinal)
            });
        }

        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal()
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = ""
        return Created("", null);
    }
}