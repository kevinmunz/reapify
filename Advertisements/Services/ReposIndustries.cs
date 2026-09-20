using Advertisements.Models.IndustryModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Advertisements.Services
{
    public interface IReposIndustries
    {
        Task Create(Industry industry);
        Task Create(Industry industry, SqlConnection connection, SqlTransaction transaction = null);
        Task<IEnumerable<Industry>> GetAll();
        Task<Industry> GetById(int industryId);
        Task Update(Industry industry);

        string ConnectionString { get; }
    }

    public class ReposIndustries : IReposIndustries
    {
        private readonly string connectionString;
        public string ConnectionString => connectionString; //Getter público para poder usar la conexión en la acción
        public ReposIndustries(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(Industry industry)
        {
            using var connection = new SqlConnection(connectionString);
            var industryId = await connection.QuerySingleAsync<int>(
                "CreateIndustry",
                new
                {
                    industry.Name
                }, commandType: System.Data.CommandType.StoredProcedure);
            industry.IndustryId = industryId;
        }

        public async Task Create(Industry industry, SqlConnection connection, SqlTransaction transaction = null)
        {
            var industryId = await connection.QuerySingleAsync<int>(
                "CreateIndustry",
                new
                {
                    industry.Name
                },
                commandType: System.Data.CommandType.StoredProcedure,
                transaction: transaction
            );

            industry.IndustryId = industryId;
        }

        public async Task<IEnumerable<Industry>> GetAll()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Industry>("GetAllIndustries", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Industry> GetById(int industryId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Industry>("GetIndustryById", new { industryId }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Update(Industry industry)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "UpdateIndustry",
                new
                {
                    industry.IndustryId,
                    industry.Name
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }


    }
}
