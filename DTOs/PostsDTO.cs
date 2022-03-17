using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using asptask.Models;

namespace asptask.DTOs;

public record PostsDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("type_of_post")]
    public string TypeOfPost { get; set; }

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    // [JsonPropertyName("created_at")]
    // public DateTimeOffset CreatedAt { get; set; }


    [JsonPropertyName("likes")]
    public List<LikesDTO> Likes { get; set; }


}


public record PostCreateDTO
{
    [JsonPropertyName("id")]
    [Required]
    public int Id { get; set; }

    [JsonPropertyName("type_of_post")]
    [Required]
    public string TypeOfPost { get; set; }

    // [JsonPropertyName("created_at")]
    // public DateTimeOffset CreatedAt { get; set; }


    [JsonPropertyName("user_id")]
    [Required]
    public int UserId { get; set; }

}

public record PostUpdateDTO
{
    [JsonPropertyName("type_of_post")]
    public string TypeOfPost { get; set; }


    // [JsonPropertyName("user_id")]
    // public int UserId { get; set; }

}