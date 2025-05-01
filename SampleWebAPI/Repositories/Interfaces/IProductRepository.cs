using SampleWebAPI.Models;
using System.Collections.Generic;

namespace SampleWebAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> Add(Product product);
        Task<Product> Update(Product product);
        Task<int> Delete(int id);
    }
}
