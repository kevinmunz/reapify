using Advertisements.Models.CreatorModels;
using Advertisements.Models.EntityModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Advertisements.Services
{
    public interface IReposEntities
    {
        Task<IEnumerable<Entity>> GetAll();
        Task<Entity> GetById(int entityId);
    }

    public class ReposEntities : IReposEntities
    {
        private readonly string connectionString;
        public ReposEntities(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Entity>> GetAll()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Entity>("GetAllEntities", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Entity> GetById(int entityId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Entity>("GetEntityById", new { entityId }, commandType: System.Data.CommandType.StoredProcedure);
        }

    }
}
