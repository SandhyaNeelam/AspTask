using asptask.DTOs;
using asptask.Models;
using asptask.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace asptask.Controllers;

[ApiController]
[Route("api/hashtag")]


public class HashtagController : ControllerBase
{
    private readonly ILogger<HashtagController> _logger;
    private readonly IHashtagRepository _hashtag;

    private readonly IPostsRepository _posts;


    public HashtagController(ILogger<HashtagController> logger, IHashtagRepository hashtag, IPostsRepository posts)
    {
        _logger = logger;
        _hashtag = hashtag;
        _posts = posts;
    }

    [HttpPost]
    public async Task<ActionResult<List<Hashtag>>> Createhashtag([FromBody] HashtagCreateDTO Data)
    {
        // var user = await _hashtag.GetById(Data.Id);
        // if (user is null)
        //     return NotFound("No Hashtags found with given user id");

        var toCreatehashtag = new Hashtag
        {
            Id = Data.Id,
            Name = Data.Name.Trim(),
        };

        var createdItem = await _hashtag.Create(toCreatehashtag);
        return StatusCode(StatusCodes.Status201Created);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Deletehashtag([FromRoute] int id)
    {
        var existing = await _hashtag.GetById(id);
        if (existing is null)
            return NotFound("No hashtags found with given id");

        await _hashtag.Delete(id);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Updatehashtag([FromRoute] int id, [FromBody] HashtagUpdateDTO Data)
    {
        var existing = await _hashtag.GetById(id);
        if (existing is null)
            return NotFound("No hashtag found to update with given id");

        var toUpdateItem = existing with
        {
            Name = Data.Name.Trim()
        };
        await _hashtag.Update(toUpdateItem);


        return NoContent();

    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Hashtag>> GetHashtagByID([FromRoute] int id)
    {
        var hashtag = await _hashtag.GetById(id);
        if (hashtag is null)
            return NotFound("No Hashtag found with given id");

        var dto = hashtag.asDto;
        dto.Posts = await _posts.GetAllForHashtag(hashtag.Id);

        return Ok(dto);

    }


}