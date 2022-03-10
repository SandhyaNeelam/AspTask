using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using asptask.DTOs;

namespace asptask.Models;

public record Likes
{
   
    public int Id { get; set; }
       public int UserId { get; set; }
    public int PostId { get; set; }

   
    public DateTimeOffset CreatedAt { get; set; }

     public LikesDTO asDto{
        get{
            return new LikesDTO{
                Id = Id,
                UserId = UserId,
                PostId= PostId,
                CreatedAt = CreatedAt
                
            };
        }
    }

}




