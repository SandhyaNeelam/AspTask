using asptask.DTOs;
using asptask.Models;
using asptask.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace asptask.Controllers;

[ApiController]
// [Route("[controller]")]
[Route("api/userdetails")]

public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _user;
    private readonly IPostsRepository _posts;



    public UserController(ILogger<UserController> logger, IUserRepository user, IPostsRepository posts)
    {
        _logger = logger;
        _user = user;
        _posts =  posts;
    }



    [HttpPost]
    public async Task<ActionResult<List<UserDTO>>> CreateUser([FromBody] UserCreateDTO Data)
    {

        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        //         return BadRequest("Gender value is not valid");

        var toCreateUser = new User
        {
            Id = Data.Id,
            Name = Data.Name.Trim(),
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Mobile = Data.Mobile,
            Email = Data.Email.ToLower().Trim(),
            Gender = Data.Gender.Trim()
        };
        var createdUser = await _user.Create(toCreateUser);
        return StatusCode(StatusCodes.Status201Created);

    }

    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
    {
        var usersList = await _user.GetList();
        var dtoList = usersList.Select(x => x.asDto);
        return Ok(dtoList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUserById([FromRoute] int id)
    {
        var user = await _user.GetById(id);
        if (user is null)
            return NotFound("No user is found with given id");
        var dto = user.asDto;

       
     dto.Posts = await _posts.GetAllForUser(user.Id);
    
        return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] UserUpdateDTO Data)
    {
        var existing = await _user.GetById(id);
        if (existing is null)
            return NotFound("No user found with given id");

        var toUpdateUser = existing with
        {
            Name = Data.Name?.Trim() ?? existing.Name,
            Mobile = Data.Mobile ?? existing.Mobile,
            Email = Data.Email?.ToLower().Trim() ?? existing.Email

        };

        var didUpdate = await _user.Update(toUpdateUser);
        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update user");

        return NoContent();

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        var existing = await _user.GetById(id);
        if (existing is null)
            return NotFound("No user found with given id");

        var didDelete = await _user.Delete(id);

        return NoContent();
    }
}
