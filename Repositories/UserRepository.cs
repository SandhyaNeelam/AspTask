using asptask.Models;
using Dapper;

namespace asptask.Repositories;

public interface IUserRepository
{
    Task<User> Create(User Item);
    Task<bool> Update(User Item);
    Task<bool> Delete(int Id);
    Task<User> GetById(int Id);
    Task<List<User>> GetList();

}

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration)
    {

    }
    public async Task<User> Create(User Item)
    {
        var createQuery = $@"INSERT INTO public.userdetails(
	id, date_of_birth, mobile, email, gender, name)
	VALUES (@Id, @DateOfBirth,@Mobile,@Email,@Gender,@Name) RETURNING *";

        using (var connection = NewConnection)
        {
            var result = await connection.QuerySingleOrDefaultAsync<User>(createQuery, Item);
            return result;

        }

    }



    public async Task<List<User>> GetList()
    {
        var getQuery = $@"SELECT * FROM userdetails";
        var connection = NewConnection;
        var res = (await connection.QueryAsync<User>(getQuery)).AsList();
        await connection.CloseAsync();
        return res;
    }


    public async Task<bool> Update(User Item)
    {
        var updateQuery = $@"UPDATE public.userdetails
	SET  name=@Name,  date_of_birth=@DateOfBirth, mobile=@Mobile, email=@Email, gender=@Gender WHERE id =@Id";

        using (var connection = NewConnection)
        {

            var rowCount = await connection.ExecuteAsync(updateQuery, Item);
            return rowCount == 1;
        }
    }

    public async Task<bool> Delete(int Id)
    {
        var deleteQuery = $@"DELETE FROM public.userdetails
	WHERE id = @Id";

        using (var connection = NewConnection)
        {

            var res = await connection.ExecuteAsync(deleteQuery, new { Id });
            return res > 0;
        }

    }

    public async Task<User> GetById(int Id)
    {
        var getIdQuery = $@"SELECT * FROM public.userdetails WHERE id=@userid;";
        using (var connection = NewConnection)
            return await connection.QuerySingleOrDefaultAsync<User>(getIdQuery, new { userid = Id });
    }
}