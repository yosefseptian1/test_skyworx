using System.Data;
using Dapper;
using Npgsql;

public interface IDbService
{
    Task<T> GetAsync<T>(string command, object parms); 
    Task<List<T>> GetAll<T>(string command, object parms );
    Task<int> EditData(string command, object parms);

    Task<int> CreateData(string command, object parms);
}

public class DbService : IDbService
{
    private readonly IDbConnection _db;

    public DbService(IConfiguration configuration)
    {
        _db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<T> GetAsync<T>(string command, object parms)
    {
        T result;

        result = (await _db.QueryAsync<T>(command, parms).ConfigureAwait(false)).FirstOrDefault();

        return result;

    }

    public async Task<List<T>> GetAll<T>(string command, object parms)
    {

        List<T> result = new List<T>();

        result = (await _db.QueryAsync<T>(command, parms)).ToList();

        return result;
    }

    public async Task<int> EditData(string command, object parms)
    {
        int result;

        result = await _db.ExecuteAsync(command, parms);

        return result;
    }

    public async Task<int> CreateData(string command, object parms)
    {
        int result;

        result = await _db.ExecuteAsync(command, parms);

        return result;
    }
}