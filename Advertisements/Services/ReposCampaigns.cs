using Advertisements.Models.CampaignModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Advertisements.Services
{
    public interface IReposCampaigns
    {
        Task Create(Campaign campaign);
        Task<IEnumerable<Campaign>> GetAll();
        Task<Campaign> GetById(int campaignId);
        Task Update(Campaign campaign);
        Task Create(Campaign campaign, SqlConnection connection, SqlTransaction transaction = null);
        Task UpdatePayments(int campaignId, decimal clientPayment, decimal creatorPayout);

        string ConnectionString { get; }
    }

    public class ReposCampaigns : IReposCampaigns
    {
        private readonly string connectionString;
        public string ConnectionString => connectionString; //Getter público para poder usar la conexión en la acción
        public ReposCampaigns(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(Campaign campaign)
        {
            using var connection = new SqlConnection(connectionString);
            var campaignId = await connection.QuerySingleAsync<int>(
                "CreateCampaign",
                new
                {
                    campaign.CreationDate,
                    campaign.ClientId,
                    campaign.Scale,
                    campaign.Currency,
                    campaign.ComissionRate,
                    campaign.ClientBudget,
                    campaign.CreatorBudget,
                    campaign.Platform,
                    campaign.Goal,
                    campaign.StartDate,
                    campaign.EndDate,
                    campaign.ClientPaymentRate,
                    campaign.CreatorPayoutRate,
                    campaign.Details,
                    campaign.GoogleFormsLink,
                    campaign.ClientPayment,
                    campaign.CreatorPayout,
                    campaign.WebhookURL,
                    campaign.SalesGrowth,
                    campaign.Stage,
                    campaign.CampaignStatus
                }, commandType: System.Data.CommandType.StoredProcedure);
            campaign.ProductId = campaignId;
        }

        public async Task Create(Campaign campaign, SqlConnection connection, SqlTransaction transaction = null)
        {
            var campaignId = await connection.QuerySingleAsync<int>(
                "CreateCampaign",
                new
                {
                    campaign.CreationDate,
                    campaign.ClientId,
                    campaign.Scale,
                    campaign.Currency,
                    campaign.ComissionRate,
                    campaign.ClientBudget,
                    campaign.CreatorBudget,
                    campaign.Platform,
                    campaign.Goal,
                    campaign.StartDate,
                    campaign.EndDate,
                    campaign.ClientPaymentRate,
                    campaign.CreatorPayoutRate,
                    campaign.Details,
                    campaign.GoogleFormsLink,
                    campaign.ClientPayment,
                    campaign.CreatorPayout,
                    campaign.WebhookURL,
                    campaign.SalesGrowth,
                    campaign.Stage,
                    campaign.CampaignStatus
                },
                commandType: System.Data.CommandType.StoredProcedure,
                transaction: transaction
            );

            campaign.ProductId = campaignId;
        }

        public async Task<IEnumerable<Campaign>> GetAll()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Campaign>("GetAllCampaigns", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Campaign> GetById(int campaignId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Campaign>("GetCampaignById", new { campaignId }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Update(Campaign campaign)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "UpdateCampaign",
                new
                {
                    campaign.ProductId,
                    campaign.CreationDate,
                    campaign.ClientId,
                    campaign.Scale,
                    campaign.Currency,
                    campaign.ComissionRate,
                    campaign.ClientBudget,
                    campaign.CreatorBudget,
                    campaign.Platform,
                    campaign.Goal,
                    campaign.StartDate,
                    campaign.EndDate,
                    campaign.ClientPaymentRate,
                    campaign.CreatorPayoutRate,
                    campaign.Details,
                    campaign.GoogleFormsLink,
                    campaign.SalesGrowth,
                    campaign.Stage,
                    campaign.WebhookURL,
                    campaign.CampaignStatus
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }
        public async Task UpdatePayments(int campaignId, decimal clientPayment, decimal creatorPayout)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "UpdateCampaignPayments",
                new
                {
                    campaignId,
                    clientPayment,
                    creatorPayout
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}