using Advertisements.Models.CampaignCreatorModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Advertisements.Services
{
    public interface IReposCampaignCreators
    {
        Task Create(CampaignCreator campaignCreator);
        Task Create(CampaignCreator campaignCreator, SqlConnection connection, SqlTransaction transaction = null);
        Task<IEnumerable<CampaignCreator>> GetAll();
        Task<CampaignCreator> GetById(int campaignCreatorId);
        Task Update(CampaignCreator campaignCreator);
        Task<IEnumerable<CampaignCreator>> GetByCampaign(int campaignId);
        Task<CampaignCreator> GetByIds(int campaignId, int creatorId);
        Task Delete(int campaignCreatorId);

        string ConnectionString { get; }
    }

    public class ReposCampaignCreators : IReposCampaignCreators
    {
        private readonly string connectionString;
        public string ConnectionString => connectionString; //Getter público para poder usar la conexión en la acción
        public ReposCampaignCreators(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(CampaignCreator campaignCreator)
        {
            using var connection = new SqlConnection(connectionString);
            var campaignCreatorId = await connection.QuerySingleAsync<int>(
                "CreateCampaignCreator",
                new
                {
                    campaignCreator.CreationDate,
                    campaignCreator.CampaignId,
                    campaignCreator.CreatorId,

                }, commandType: System.Data.CommandType.StoredProcedure);
            campaignCreator.CampaignCreatorId = campaignCreatorId;
        }

        public async Task Create(CampaignCreator campaignCreator, SqlConnection connection, SqlTransaction transaction = null)
        {
            var campaignCreatorId = await connection.QuerySingleAsync<int>(
                "CreateCampaignCreator",
                new
                {
                    campaignCreator.CreationDate,
                    campaignCreator.CampaignId,
                    campaignCreator.CreatorId,
                },
                commandType: System.Data.CommandType.StoredProcedure,
                transaction: transaction
            );

            campaignCreator.CampaignCreatorId = campaignCreatorId;
        }

        public async Task<IEnumerable<CampaignCreator>> GetAll()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<CampaignCreator>("GetAllCampaignCreators", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CampaignCreator>> GetByCampaign(int campaignId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<CampaignCreator>("GetCampaignCreatorsByCampaign", new { campaignId }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<CampaignCreator> GetById(int campaignCreatorId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<CampaignCreator>("GetCampaignCreatorById",
                                                                            new { campaignCreatorId },
                                                                            commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<CampaignCreator> GetByIds(int campaignId, int creatorId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<CampaignCreator>("GetCampaignCreatorByIds",
                                                                            new { campaignId, creatorId },
                                                                            commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Update(CampaignCreator campaignCreator)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "UpdateCampaignCreator",
                new
                {
                    campaignCreator.CampaignCreatorId,
                    campaignCreator.CreationDate,
                    campaignCreator.CampaignId,
                    campaignCreator.CreatorId,
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Delete(int campaignCreatorId)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                                        "DeleteCampaignCreator",
                                        new{ campaignCreatorId },
                                        commandType: System.Data.CommandType.StoredProcedure);
        }


    }
}
