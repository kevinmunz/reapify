using Advertisements.Models.ClientModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Advertisements.Services
{
    public interface IReposClients
    {
        Task Create(Client client);
        Task Create(Client client, SqlConnection connection, SqlTransaction transaction = null);
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetById(int clientId);
        Task Update(Client client);

        string ConnectionString { get; }
    }

    public class ReposClients : IReposClients
    {
        private readonly string connectionString;
        public string ConnectionString => connectionString; //Getter público para poder usar la conexión en la acción
        public ReposClients(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(Client client)
        {
            using var connection = new SqlConnection(connectionString);
            var clientId = await connection.QuerySingleAsync<int>(
                "CreateClient",
                new {
                    client.CreationDate,
                    client.Name,
                    client.StateId,
                    client.IndustryId,
                    client.ReferenceLink,
                    client.ContactName,
                    client.ContactEmail,
                    client.ContactPhone,
                    client.NIT,
                    client.TaxAddress,
                    
                }, commandType: System.Data.CommandType.StoredProcedure);
            client.EntityId = clientId;
        }

        public async Task Create(Client client, SqlConnection connection, SqlTransaction transaction = null)
        {
            var clientId = await connection.QuerySingleAsync<int>(
                "CreateClient",
                new
                {
                    client.CreationDate,
                    client.Name,
                    client.StateId,
                    client.IndustryId,
                    client.ReferenceLink,
                    client.ContactName,
                    client.ContactEmail,
                    client.ContactPhone,
                    client.NIT,
                    client.TaxAddress,
                },
                commandType: System.Data.CommandType.StoredProcedure,
                transaction: transaction
            );

            client.EntityId = clientId;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Client>("GetAllClients", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Client> GetById(int clientId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Client>("GetClientById", new { clientId }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Update(Client client)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "UpdateClient",
                new {
                    client.EntityId,
                    client.CreationDate,
                    client.Name,
                    client.StateId,
                    client.IndustryId,
                    client.ReferenceLink,
                    client.ContactName,
                    client.ContactEmail,
                    client.ContactPhone,
                    client.NIT,
                    client.TaxAddress,
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }


    }
}
