using asptask.DTOs;
using asptask.Models;
using asptask.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace asptask.Controllers;

[ApiController]
[Route("api/posts")]

public class PostsController : ControllerBase
{
    private readonly ILogger<PostsController> _logger;
    private readonly IPostsRepository _posts;
    private readonly IUserRepository _user;
    private readonly ILikesRepository _likes;


    public PostsController(ILogger<PostsController> logger, IPostsRepository posts, IUserRepository user, ILikesRepository likes)
    {
        _logger = logger;
        _posts = posts;
        _user = user;
        _likes = likes;
    }

    [HttpPost]
    public async Task<ActionResult<List<Posts>>> CreatePosts([FromBody] PostCreateDTO Data)
    {
        var user = await _user.GetById(Data.UserId);
        if (user is null)
            return NotFound("No post found with given user id");

        var toCreatePosts = new Posts
        {
            Id = Data.Id,
            TypeOfPost = Data.TypeOfPost.Trim(),
            UserId = Data.UserId
        };

        var createdItem = await _posts.Create(toCreatePosts);
        return StatusCode(StatusCodes.Status201Created, createdItem);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePosts([FromRoute] int id)
    {
        var existing = await _posts.GetById(id);
        if (existing is null)
            return NotFound("No post found with given id");

        await _user.Delete(id);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePosts([FromRoute] int id, [FromBody] PostUpdateDTO Data)
    {
        var existing = await _posts.GetById(id);
        if (existing is null)
            return NotFound("No posts found to update with given id");

        var toUpdateItem = existing with
        {
            TypeOfPost = Data.TypeOfPost.Trim(),
            // UserId = Data.UserId
        };
        await _posts.Update(toUpdateItem);


        return NoContent();

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostsDTO>> GetUserById([FromRoute] int id)
    {
        var post = await _posts.GetById(id);
        if (post is null)
            return NotFound("No post is found with given id");
        var dto = post.asDto;

        dto.Likes = await _likes.GetAllForPosts(post.Id);
        

        return Ok(dto);
    }


    [HttpGet]

    public async Task<ActionResult<List<PostsDTO>>> GetAllLikes()
    {
        var likesList = await _posts.GetList();
        var dtoList = likesList.Select(x => x.asDto);
        return Ok(dtoList);

    }














}