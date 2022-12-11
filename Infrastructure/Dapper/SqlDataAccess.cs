using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Dapper;

public class SqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringKey)
    {
        string connectionString = GetConnectionString(connectionStringKey);

        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            List<T> rows = connection.Query<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure).ToList();

            return rows;
        }
    }

    public void SaveData<T>(string storedProcedure, T parameters, string connectionStringKey)
    {
        string connectionString = GetConnectionString(connectionStringKey);

        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            connection.Execute(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure);
        }
    }

    public string GetConnectionString(string name)
    {
        return _config.GetConnectionString(name);
    }

}
