using BudgetManagement.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BudgetManagement.Services
{
    public interface IAccountTypeRepository
    {
        void Create(AccountType accountType);
    }

    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly string connectionString;
        public AccountTypeRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Create(AccountType accountType)
        {
            using var connection = new SqlConnection(connectionString);

            var id = connection.QuerySingle<int>($@"INSERT INTO AccountType (Name, OrderNumber, UserId) 
                                                    VALUES(@Name, @UserId, 0);
                                                    SELECT SCOPE_IDENTITY();", accountType);

            accountType.Id = id;
        }
    }
}
