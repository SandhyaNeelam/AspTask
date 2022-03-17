using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using asptask.DTOs;

namespace asptask.Models;

public record Posts
{
    
    public int Id { get; set; }

    public string TypeOfPost { get; set; }

    public int UserId { get; set; }

//    public DateTimeOffset CreatedAt{ get; set; }


   public PostsDTO asDto{
        get{
            return new PostsDTO{
                Id = Id,
                TypeOfPost= TypeOfPost,
                UserId = UserId
                // CreatedAt = CreatedAt
                
            };
        }
    }


}

