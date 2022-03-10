using asptask.DTOs;
using asptask.Models;
using asptask.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace asptask.Controllers;

[ApiController]
[Route("api/likes")]

public class LikesController : ControllerBase
{

    private readonly ILogger<LikesController> _logger;

    private readonly ILikesRepository _likes;
    private readonly IPostsRepository _posts;




    public LikesController(ILogger<LikesController> logger, ILikesRepository likes, IPostsRepository posts)
    {
        _logger = logger;
        _likes = likes;
        _posts = posts;

    }


    [HttpPost]
    public async Task<ActionResult<LikesDTO>> CreateLikes([FromBody] LikesCreateDTO Data)
    {
        var toLike = new Likes
        {
            Id = Data.Id,
            UserId = Data.UserId,
            PostId = Data.PostId

        };

         var createdItem = await _likes.Create(toLike);
        return StatusCode(StatusCodes.Status201Created, createdItem);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLikes([FromRoute] int id)
    {
        var existing = await _likes.GetById(id);
        if (existing is null)
            return NotFound("No one liked found with given id");

        await _likes.Delete(id);

        return NoContent();
    }














}









