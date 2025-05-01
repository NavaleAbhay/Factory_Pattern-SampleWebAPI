using SampleWebAPI.Data;
using SampleWebAPI.Models;
using SampleWebAPI.Repositories.Interfaces;

namespace SampleWebAPI.Repositories
{
    public static class ProductRepositoryFactory
    {
        public static IProductRepository Create(string type, string connectionString = null, ProductDbContext context = null)
        {
            return type switch
            {
                "File" => new FileProductRepository(),
                "AdoNet" => new AdoNetProductRepository(connectionString),
                "EntityFramework" => new EfProductRepository(context),
                "External" => new ExternalProductRepository(new HttpClient
                {
                    BaseAddress = new Uri("http://localhost:9000/api/products")
                }),
                _ => throw new ArgumentException("Invalid repository type")
            };
        }
    }
}   
