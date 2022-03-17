using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace asptask.DTOs;

public record HashtagDTO
{

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }


    [JsonPropertyName("posts")]
    public List<PostsDTO> Posts { get; set; }

}


public record HashtagCreateDTO
{
    [JsonPropertyName("id")]
    [Required]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; }

   

}

public record HashtagUpdateDTO
{

    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; }

  

}