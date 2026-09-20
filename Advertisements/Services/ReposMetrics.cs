        using Advertisements.Models.MetricModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Advertisements.Services
{
    public interface IReposMetrics
    {
        Task Create(Metric metric);
        Task Create(Metric metric, SqlConnection connection, SqlTransaction transaction = null);
        Task<IEnumerable<Metric>> GetAll();
        Task<Metric> GetById(int metricId);
        Task Update(Metric metric);
        Task<IEnumerable<Metric>> GetByCampaign(int campaignId);
        Task UpdatePayments(int metricId, decimal calculatedClientPayment, decimal calculatedCreatorPayout);
        Task<bool> RecalculateCampaignPayments(int campaignId);
        Task<Metric> GetByCampaignCreator(int campaignCreatorId);

        string ConnectionString { get; }
    }

    public class ReposMetrics : IReposMetrics
    {
        private readonly string connectionString;
        public string ConnectionString => connectionString; //Getter público para poder usar la conexión en la acción
        public ReposMetrics(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(Metric metric)
        {
            using var connection = new SqlConnection(connectionString);
            var metricId = await connection.QuerySingleAsync<int>(
                "CreateMetric",
                new
                {
                    metric.CreationDate,
                    metric.SubmissionTimestamp,
                    metric.CampaignCreatorId,
                    metric.ScreenshotLink,
                    metric.VideoLink,
                    metric.NumViews,
                    metric.NumLikes,
                    metric.NumComments,
                    metric.NumShares,
                    metric.NumSaves,
                    metric.VideoDuration,
                    metric.QRCodeLink,
                    metric.CalculatedClientPayment,
                    metric.CalculatedCreatorPayout,
                    
                }, commandType: System.Data.CommandType.StoredProcedure);
            metric.ProductId = metricId;
        }

        public async Task Create(Metric metric, SqlConnection connection, SqlTransaction transaction = null)
        {
            var metricId = await connection.QuerySingleAsync<int>(
                "CreateMetric",
                new
                {
                    metric.CreationDate,
                    metric.SubmissionTimestamp,
                    metric.CampaignCreatorId,
                    metric.ScreenshotLink,
                    metric.VideoLink,
                    metric.NumViews,
                    metric.NumLikes,
                    metric.NumComments,
                    metric.NumShares,
                    metric.NumSaves,
                    metric.VideoDuration,
                    metric.QRCodeLink,
                    metric.CalculatedClientPayment,
                    metric.CalculatedCreatorPayout,
                },
                commandType: System.Data.CommandType.StoredProcedure,
                transaction: transaction
            );

            metric.ProductId = metricId;
        }

        public async Task<IEnumerable<Metric>> GetAll()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Metric>("GetAllMetrics", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Metric>> GetByCampaign(int campaignId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Metric>("GetMetricsByCampaign", new { campaignId },commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Metric> GetById(int metricId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Metric>("GetMetricById", new { metricId }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Metric> GetByCampaignCreator(int campaignCreatorId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Metric>("GetMetricByCampaignCreator", new { campaignCreatorId }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Update(Metric metric)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "UpdateMetric",
                new
                {
                    metric.ProductId,
                    metric.CreationDate,
                    metric.SubmissionTimestamp,
                    metric.CampaignCreatorId,
                    metric.ScreenshotLink,
                    metric.VideoLink,
                    metric.NumViews,
                    metric.NumLikes,
                    metric.NumComments,
                    metric.NumShares,
                    metric.NumSaves,
                    metric.VideoDuration,
                    metric.QRCodeLink,
                    metric.CalculatedClientPayment,
                    metric.CalculatedCreatorPayout,
                    metric.MetricStatus,
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<bool> RecalculateCampaignPayments(int campaignId)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
            var campaign = await connection.QueryFirstOrDefaultAsync<Advertisements.Models.CampaignModels.Campaign>(
                "GetCampaignById", new { campaignId }, transaction, commandType: System.Data.CommandType.StoredProcedure);
            if (campaign is null) return false;
            var metrics = (await connection.QueryAsync<Metric>("GetMetricsByCampaign", new { campaignId },
                transaction, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            CampaignPaymentCalculator.Calculate(metrics, campaign.ClientBudget, campaign.ClientPaymentRate, campaign.ComissionRate);
            foreach (var metric in metrics.Where(m => m.MetricStatus != Advertisements.Models.Enums.MetricStatus.Settled))
                await connection.ExecuteAsync("UpdateMetricPayments", new {
                    metricId = metric.ProductId, calculatedClientPayment = metric.CalculatedClientPayment,
                    calculatedCreatorPayout = metric.CalculatedCreatorPayout
                }, transaction, commandType: System.Data.CommandType.StoredProcedure);
            await connection.ExecuteAsync("UpdateCampaignPayments", new {
                campaignId, clientPayment = metrics.Sum(m => m.CalculatedClientPayment),
                creatorPayout = metrics.Sum(m => m.CalculatedCreatorPayout)
            }, transaction, commandType: System.Data.CommandType.StoredProcedure);
            transaction.Commit();
            return true;
        }
        public async Task UpdatePayments(int metricId, decimal calculatedClientPayment, decimal calculatedCreatorPayout)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "UpdateMetricPayments",
                new
                {
                    metricId,
                    calculatedClientPayment,
                    calculatedCreatorPayout
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

    }
}
