using SampleWebAPI.Models;
using SampleWebAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SampleWebAPI.Repositories
{
    public class AdoNetProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public AdoNetProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = new List<Product>();
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand("SELECT * FROM Product", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                products.Add(new Product
                {
                    ProductID = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Price = reader.GetDecimal(2)
                });
            }
            return products;
        }

        public async Task<Product> GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand("SELECT * FROM Product WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Product
                {
                    ProductID = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Price = reader.GetDecimal(2)
                };
            }
            return null;
        }

        public async Task<Product> Add(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand("INSERT INTO Product (Title, Price) OUTPUT INSERTED.Id VALUES (@Name, @Price)", connection);
            command.Parameters.AddWithValue("@Name", product.Title);
            command.Parameters.AddWithValue("@Price", product.Price);
            var insertedId = (int)await command.ExecuteScalarAsync();
            product.ProductID = insertedId;
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand("UPDATE Product SET Title = @Name, Price = @Price WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", product.ProductID);
            command.Parameters.AddWithValue("@Name", product.Title);
            command.Parameters.AddWithValue("@Price", product.Price);
            await command.ExecuteNonQueryAsync();
            return product;
        }

        public async Task<int> Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand("DELETE FROM Product WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            return await command.ExecuteNonQueryAsync();
        }
    }
}
