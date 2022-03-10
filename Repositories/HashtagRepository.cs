using asptask.Controllers;
using asptask.Models;
using Dapper;

namespace asptask.Repositories;

public interface IHashtagRepository
{
    Task<Hashtag> Create(Hashtag Item);
   Task Update(Hashtag Item);
   Task Delete(int Id);
   Task<List<Hashtag>> GetAll(int id);
   Task<Hashtag> GetById(int Id);
    
}

public class HashtagRepository : BaseRepository, IHashtagRepository
{
    public HashtagRepository(IConfiguration configuration): base(configuration)
    {
        
    }

    public async Task<Hashtag> Create(Hashtag Item)
    {

    var createQuery = $@"INSERT INTO public.hashtag(id, name) VALUES (@Id, @Name) RETURNING *";
    using(var connection = NewConnection)
        return await connection.QuerySingleAsync<Hashtag>(createQuery,Item);
    
    }



    public async Task Delete(int Id)
    {
        var deleteQuery =$@"DELETE FROM public.hashtag WHERE id = @Id";

        using(var connection = NewConnection)
        await connection.ExecuteAsync(deleteQuery, new {Id});
    }


    public async Task<List<Hashtag>> GetAll(int Id)
    {
        var getallQuery = $@"SELECT * FROM hashtag WHERE id = @Id";

        using(var connection = NewConnection)
        return (await connection.QueryAsync<Hashtag>(getallQuery, new {Id})).AsList();
    }

    public async Task<Hashtag> GetById(int Id)
    {
        var getQuery = $@"SELECT * FROM hashtag WHERE id = @Id";

        using(var connection = NewConnection)
        return await connection.QuerySingleOrDefaultAsync<Hashtag>(getQuery, new{Id});
    }

    public async Task Update(Hashtag Item)
    {
        var updateQuery = $@"UPDATE hashtag SET name= @Name  WHERE id=@Id";
        using(var connection = NewConnection)
        await connection.ExecuteAsync(updateQuery, Item);
    }
}

