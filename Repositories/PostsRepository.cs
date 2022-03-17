using asptask.DTOs;
using asptask.Models;
using Dapper;

namespace asptask.Repositories;

public interface IPostsRepository
{
    Task<Posts> GetById(int Id);
    Task<Posts> Create(Posts Item);
    Task Update(Posts Item);
    Task Delete(int Id);
    Task<List<Posts>> GetAllForUser(int Id);
    Task<List<Posts>> GetList();
    Task<List<PostsDTO>> GetAllForHashtag(int id);
}

public class PostRepository : BaseRepository, IPostsRepository
{
    public PostRepository(IConfiguration configuration) : base(configuration)
    {

    }

    public async Task<Posts> Create(Posts Item)
    {

        var createQuery = $@"INSERT INTO public.posts(id, type_of_post, user_id) VALUES (@Id, @TypeOfPost, @UserId) RETURNING *;";
        using (var connection = NewConnection)
            return await connection.QuerySingleAsync<Posts>(createQuery, Item);

    }

    public async Task Delete(int Id)
    {
        var deleteQuery = $@"DELETE FROM posts WHERE id =@Id";

        using (var connection = NewConnection)
            await connection.ExecuteAsync(deleteQuery, new { Id });
    }



    public async Task<List<Posts>> GetAllForUser(int Id)
    {
        var getallQuery = $@"SELECT * FROM posts WHERE user_id = @Id";

        using (var connection = NewConnection)
            return (await connection.QueryAsync<Posts>(getallQuery, new { Id })).AsList();
    }

    public async Task<Posts> GetById(int Id)
    {
        var getQuery = $@"SELECT * FROM posts WHERE id = @Id";

        using (var connection = NewConnection)
            return await connection.QuerySingleOrDefaultAsync<Posts>(getQuery, new { Id });
    }

    public async Task<List<Posts>> GetList()
    {
        var getQuery = $@"SELECT * FROM posts";
        var connection = NewConnection;
        var res = (await connection.QueryAsync<Posts>(getQuery)).AsList();
        await connection.CloseAsync();
        return res;
    }

    public async Task Update(Posts Item)
    {
        var updateQuery = $@"UPDATE posts SET type_of_post, useer_id = @UserId = @TypeOfPost WHERE id=@Id";
        using (var connection = NewConnection)
            await connection.ExecuteAsync(updateQuery, Item);
    }


    public async Task<List<PostsDTO>> GetAllForHashtag(int HashtagId)
    {
        var query = $@"SELECT * FROM post_hash ph 
        LEFT JOIN posts p ON p.id = ph.post_id WHERE hashtag_id =@HashtagId";

        using (var connection = NewConnection)
            return (await connection.QueryAsync<PostsDTO>(query, new { HashtagId })).AsList();

    }




}