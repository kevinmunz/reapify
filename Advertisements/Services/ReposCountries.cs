using Advertisements.Models.CountryModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Advertisements.Services
{
    public interface IReposCountries
    {
        Task Create(Country country);
        Task<IEnumerable<Country>> GetAll();
        Task<Country> GetById(int countryId);
        Task Update(Country country);

        string ConnectionString { get; }
    }

    public class ReposCountries : IReposCountries
    {
        private readonly string connectionString;
        public string ConnectionString => connectionString; //Getter público para poder usar la conexión en la acción
        public ReposCountries(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(Country country)
        {
            using var connection = new SqlConnection(connectionString);
            var countryId = await connection.QuerySingleAsync<int>(
                "CreateCountry",
                new
                {
                    country.Name,
                    country.Currency,
                    country.Region,
                    country.Subregion
                }, commandType: System.Data.CommandType.StoredProcedure);
            country.CountryId = countryId;
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Country>("GetAllCountries", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Country> GetById(int countryId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Country>("GetCountryById", new { countryId }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Update(Country country)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "UpdateCountry",
                new
                {
                    country.CountryId,
                    country.Name,
                    country.Currency,
                    country.Region,
                    country.Subregion
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }


    }
}
