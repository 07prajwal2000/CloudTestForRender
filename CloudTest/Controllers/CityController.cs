using Microsoft.AspNetCore.Mvc;

namespace CloudTest.Controllers;

[ApiController]
[Route("[controller]")]
public class CityController : ControllerBase
{
    private static List<City> Cities = new();

    [HttpGet("all")]
    public IActionResult GetCities()
    {
        return Ok(Cities);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetCity(Guid id)
    {
        return Ok(Cities.FirstOrDefault(x => x.Id == id));
    }
    
    [HttpPost("add")]
    public IActionResult AddCity(AddCityDto dto)
    {
        if (Cities.Any(x => x.Name == dto.Name))
        {
            return BadRequest(new
            {
                Message = "City already exists"
            });
        }
        var city = new City(Guid.NewGuid(), dto.Name, dto.Country);
        Cities.Add(city);
        return Ok(Cities);
    }
    
    [HttpDelete("delete")]
    public IActionResult DeleteCity(Guid id)
    {
        var city = Cities.FirstOrDefault(x => x.Id == id);
        if (city is null)
        {
            return BadRequest(new
            {
                Message = "City not found."
            });
        }
        Cities.Remove(city);
        return Ok(Cities);
    }

}

public record AddCityDto(string Name, string Country);
public record City(Guid Id, string Name, string Country);