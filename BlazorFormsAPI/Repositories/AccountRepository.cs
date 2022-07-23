using BlazorFormsAPI.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace BlazorFormsAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connString;

        public AccountRepository(IConfiguration configuration)
        {
            _connString = configuration["ConnectionStrings:MySqlConn"];
        }

        public async Task<User> GetUserByName(string name)
        {
            string[] windowsUserArray = name.Split('\\');
            using var conn = new MySqlConnection(_connString);
            string query = @$"select Id, Name, Email, Rol 
                              from user 
                              where SUBSTRING_INDEX(Name, '\\', 1) = '{windowsUserArray[0]}' and
	                                SUBSTRING_INDEX(Name, '\\', -1) = '{windowsUserArray[1]}' and 
                                    Active = 1";
            return await conn.QueryFirstOrDefaultAsync<User>(query);
        }
    }
}
