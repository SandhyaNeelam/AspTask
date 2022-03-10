using asptask.DTOs;

namespace asptask.Models;


public record User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public long Mobile { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    

    public UserDTO asDto{
        get{
            return new UserDTO{
                Id = Id,
                Name = Name,
                Mobile = Mobile,
                Email = Email,
                Gender = Gender
            };
        }
    }


    
}