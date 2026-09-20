using Advertisements.Models.EntityModels;
using Advertisements.Models.ProductModels;
using Dapper;
using Microsoft.Data.SqlClient;


namespace Advertisements.Services
{
    public interface IReposProducts
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int productId);
    }

    public class ReposProducts : IReposProducts
    {
        private readonly string connectionString;
        public ReposProducts(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Product>("GetAllProducts", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Product> GetById(int productId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Product>("GetProductById", new { productId }, commandType: System.Data.CommandType.StoredProcedure);
        }

    }
}
