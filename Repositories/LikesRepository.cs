using asptask.DTOs;
using asptask.Models;
using Dapper;

namespace asptask.Repositories;

public interface ILikesRepository
{

    Task<Likes> GetById(int Id);
    Task<Likes> Create(Likes Item);
    Task Delete(int Id);
    Task<List<LikesDTO>> GetAllForPosts(int Id);
}

public class LikesRepository : BaseRepository, ILikesRepository
{
    public LikesRepository(IConfiguration configuration) : base(configuration)
    {

    }

    public async Task<Likes> Create(Likes Item)
    {
        var createQuery = $@"INSERT INTO public.likes(id, user_id, post_id, created_at) VALUES (@Id, @UserId, @PostId, @CreatedAt) RETURNING *;";
        using (var connection = NewConnection)
            return await connection.QuerySingleAsync<Likes>(createQuery, Item);
    }

    public async Task Delete(int Id)
    {
        var deleteQuery = $@"DELETE FROM public.likes WHERE id = @Id;";

        using (var connection = NewConnection)
            await connection.ExecuteAsync(deleteQuery, new { Id });
    }

    public async Task<List<LikesDTO>> GetAllForPosts(int Id)
    {
        var getallQuery = $@"SELECT * FROM likes WHERE id = @post_id";

        using (var connection = NewConnection)
            return (await connection.QueryAsync<LikesDTO>(getallQuery, new {post_id=Id})).AsList();
    }



    public async Task<Likes> GetById(int Id)
    {
        var getQuery = $@"SELECT * FROM likes WHERE id = @Id";

        using (var connection = NewConnection)
            return await connection.QuerySingleOrDefaultAsync<Likes>(getQuery, new { Id });
    }



}

