using BudgetManagement.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BudgetManagement.Services
{
    public interface IAccountTypeRepository
    {
        Task Create(AccountType accountType);
        Task<bool> Exists(string name, int userId);
    }

    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly string connectionString;
        public AccountTypeRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(AccountType accountType)
        {
            using var connection = new SqlConnection(connectionString);

            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO AccountType (Name, OrderNumber, UserId) 
                VALUES(@Name, @UserId, 0);
                 SELECT SCOPE_IDENTITY();", 
                accountType);

            accountType.Id = id;
        }

        public async Task<bool> Exists(string name, int userId)
        {
            using var connection = new SqlConnection(connectionString);
            var exists = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 1
                FROM AccountType
                WHERE Name = @Name
                AND UserId = @UserId;", 
                new {name, userId} );

            return exists == 1;
        }
    }
}
