using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace asptask.DTOs;

public record LikesDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("post_id")]
    public int PostId { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }
}



public record LikesCreateDTO
{
    [JsonPropertyName("id")]
    [Required]
    public int Id { get; set; }

    [JsonPropertyName("user_id")]
    [Required]
    public int UserId { get; set; }

    [JsonPropertyName("post_id")]
    [Required]
    public int PostId { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }
}