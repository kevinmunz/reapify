using Advertisements.Models.CreatorModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Advertisements.Services
{
    public interface IReposCreators
    {
        Task Create(Creator creator);
        Task<IEnumerable<Creator>> GetAll();
        Task<Creator> GetById(int creatorId);
        Task Update(Creator creator);
        Task Create(Creator creator, SqlConnection connection, SqlTransaction transaction = null);
        Task<Creator> GetByEmail(string email);

        string ConnectionString { get; }
    }

    public class ReposCreators : IReposCreators
    {
        private readonly string connectionString;
        public string ConnectionString => connectionString; //Getter público para poder usar la conexión en la acción
        public ReposCreators(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(Creator creator)
        {
            using var connection = new SqlConnection(connectionString);
            var creatorId = await connection.QuerySingleAsync<int>(
                "CreateCreator",
                new
                {
                    creator.CreationDate,
                    creator.Name,
                    creator.StateId,
                    creator.ContentType,
                    creator.FirstName,
                    creator.LastName,
                    creator.CI,
                    creator.RegistrationPhone,
                    creator.RegistrationEmail,
                    creator.FollowerRange,
                    creator.CreatorStatus
                }, commandType: System.Data.CommandType.StoredProcedure);
            creator.EntityId = creatorId;
        }

        public async Task Create(Creator creator, SqlConnection connection, SqlTransaction transaction = null)
        {
            var creatorId = await connection.QuerySingleAsync<int>(
                "CreateCreator",
                new
                {
                    creator.CreationDate,
                    creator.Name,
                    creator.StateId,
                    creator.ContentType,
                    creator.FirstName,
                    creator.LastName,
                    creator.CI,
                    creator.RegistrationPhone,
                    creator.RegistrationEmail,
                    creator.FollowerRange,
                    creator.CreatorStatus
                },
                commandType: System.Data.CommandType.StoredProcedure,
                transaction: transaction
            );

            creator.EntityId = creatorId;
        }

        public async Task<IEnumerable<Creator>> GetAll()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Creator>("GetAllCreators", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Creator> GetById(int creatorId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Creator>("GetCreatorById", new { creatorId }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Creator> GetByEmail(string email)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Creator>("GetCreatorByEmail", new { email }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Update(Creator creator)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "UpdateCreator",
                new
                {
                    creator.EntityId,
                    creator.CreationDate,
                    creator.Name,
                    creator.StateId,
                    creator.ContentType,
                    creator.FirstName,
                    creator.LastName,
                    creator.CI,
                    creator.RegistrationPhone,
                    creator.RegistrationEmail,
                    creator.FollowerRange,
                    creator.CreatorStatus
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
