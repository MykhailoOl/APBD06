using Microsoft.Data.SqlClient;
using Tutorial6.Models;
using Tutorial6.Models.DTOs;

namespace Tutorial6.Repositories;

public class AnimalRepository
{
    public List<Animal> getListOfAnimals(IConfiguration _configuration)
    {
        // Open connection
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        // Create command
        using SqlCommand command = new SqlCommand();
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

        return animals;
    }

    public void AddAnimal(IConfiguration _configuration,AddAnimal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "INSERT INTO Animal VALUES (@animalName,@animalDescription,@animalCategory,@animalArea)";
        command.Parameters.AddWithValue("@animalName", animal.Name);
        command.Parameters.AddWithValue("@animalDescription", animal.Description);
        command.Parameters.AddWithValue("@animalCategory", animal.Category);
        command.Parameters.AddWithValue("@animalArea", animal.Area);
        command.ExecuteNonQuery();
    }

    public void UpdateAnimal(IConfiguration _configuration,Animal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText= "UPDATE ANIMAL SET Name=@animalName, Description=@animalDescription, " + "Category=@animalCategory, Area=@animalArea WHERE AnimalId=@animalId";
        command.Parameters.AddWithValue("@animalName", animal.Name);
        command.Parameters.AddWithValue("@animalDescription", animal.Description);
        command.Parameters.AddWithValue("@animalCategory", animal.Category);
        command.Parameters.AddWithValue("@animalArea", animal.Area);
        command.Parameters.AddWithValue("@animalId", animal.IdAnimal);
        command.ExecuteNonQuery();
    }

    public void DeleteAnimal(IConfiguration _configuration, int id)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText= "DELETE FROM ANIMAL WHERE AnimalId=@animalId";
        command.Parameters.AddWithValue("@animalId", id);
        command.ExecuteNonQuery();
    }
}