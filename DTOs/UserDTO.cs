using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using asptask.Models;

namespace asptask.DTOs;

public record UserDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("user_name")]
    public string Name { get; set; }

    // [JsonPropertyName("date_of_birth")]
    // public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }
    

    [JsonPropertyName("posts")]
    public List<Posts> Posts { get; set; }
}



public record UserCreateDTO
{
   
   [JsonPropertyName("id")]
    [Required]
    public int Id { get; set; }

    [JsonPropertyName("user_name")]
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [JsonPropertyName("date_of_birth")]
    [Required]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("mobile")]
    [Required]
    public long Mobile { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("gender")]
    [Required]
    public string Gender { get; set; }
}


public record UserUpdateDTO
{

    [JsonPropertyName("name")]
    public string Name { get; set; }


    [JsonPropertyName("mobile")]
    public long? Mobile { get; set; } = null;

    [JsonPropertyName("email")]
    [MaxLength(255)]
    public string Email { get; set; }

}