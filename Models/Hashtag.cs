using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using asptask.DTOs;

namespace asptask.Models;

public record Hashtag
{

   
    public int Id { get; set; }
    public string Name { get; set; }
    // public int PostId { get; set; }




public HashtagDTO asDto{
        get{
            return new HashtagDTO{
                Id = Id,
                Name = Name,
                // PostId = PostId
                
            };
        }
    }



}


