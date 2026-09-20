using Advertisements.Models.StateModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Advertisements.Services
{
    public interface IReposStates
    {
        Task Create(State state);
        Task<IEnumerable<State>> GetAll();
        Task<State> GetById(int stateId);
        Task Update(State state);
        Task<IEnumerable<State>> GetByCountry(int countryId);

        string ConnectionString { get; }
    }

    public class ReposStates : IReposStates
    {
        private readonly string connectionString;
        public string ConnectionString => connectionString; //Getter público para poder usar la conexión en la acción
        public ReposStates(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(State state)
        {
            using var connection = new SqlConnection(connectionString);
            var stateId = await connection.QuerySingleAsync<int>(
                "CreateState",
                new
                {
                    state.CountryId,
                    state.Name,
                    state.Type,
                    state.TimeZone
                }, commandType: System.Data.CommandType.StoredProcedure);
            state.StateId = stateId;
        }

        public async Task<IEnumerable<State>> GetAll()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<State>("GetAllStates", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<State>> GetByCountry(int countryId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<State>("GetStatesByCountry", new { countryId }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<State> GetById(int stateId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<State>("GetStateById", new { stateId }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Update(State state)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "UpdateState",
                new
                {
                    state.StateId,
                    state.CountryId,
                    state.Name,
                    state.Type,
                    state.TimeZone
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }


    }
}
