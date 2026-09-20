using Advertisements.Models.TransactionModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Advertisements.Services
{
    public interface IReposTransactions
    {
        Task Create(Transaction transaction);
        Task<IEnumerable<Transaction>> GetAll();
        Task<Transaction> GetById(int transactionId);
        Task Update(Transaction transaction);
        Task<Transaction> Exists(int entityId, int productId);
        Task<Transaction> GetByIds(int entityId, int productId);

        string ConnectionString { get; }
    }

    public class ReposTransactions : IReposTransactions
    {
        private readonly string connectionString;
        public string ConnectionString => connectionString; //Getter público para poder usar la conexión en la acción
        public ReposTransactions(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(Transaction transaction)
        {
            using var connection = new SqlConnection(connectionString);
            var transactionId = await connection.QuerySingleAsync<int>(
                "CreateTransaction",
                new
                {
                    transaction.CreationDate,
                    transaction.Amount,
                    transaction.Type,
                    transaction.Direction,
                    transaction.EntityId,
                    transaction.ProductId,
                    transaction.Description

                }, commandType: System.Data.CommandType.StoredProcedure);
            transaction.TransactionId = transactionId;
        }

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Transaction>("GetAllTransactions", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Transaction> GetById(int transactionId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Transaction>("GetTransactionById", new { transactionId }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Transaction> GetByIds(int entityId, int productId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Transaction>(
                                                                        "GetTransactionByIds",
                                                                        new
                                                                        {
                                                                            entityId,
                                                                            productId
                                                                        },
                                                                        commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Transaction> Exists(int entityId, int productId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Transaction>(
                                                                        "ExistsTransaction",
                                                                        new {
                                                                            entityId,
                                                                            productId
                                                                        },
                                                                        commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Update(Transaction transaction)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "UpdateTransaction",
                new
                {
                    transaction.TransactionId,
                    transaction.CreationDate,
                    transaction.Amount,
                    transaction.Type,
                    transaction.Direction,
                    transaction.EntityId,
                    transaction.ProductId,
                    transaction.Description
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }


    }
}
