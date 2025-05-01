using SampleWebAPI.Models;
using SampleWebAPI.Repositories.Interfaces;
using System.Buffers.Text;
using System.Net;
using System.Net.Http;

namespace SampleWebAPI.Repositories
{
    public class ExternalProductRepository : IProductRepository
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:9000/api/products";

        public ExternalProductRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Product>>(BaseUrl);
            return response ?? new List<Product>();
        }

        public async Task<Product> GetById(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Product>($"{BaseUrl}/{id}");
            return response;
        }

        public async Task<Product> Add(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, product);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                return product;
            }
            throw new InvalidOperationException("Failed to add the product.");
        }

        public async Task<Product> Update(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{product.ProductID}", product);
            response.EnsureSuccessStatusCode();
            // Fix: Return the product directly instead of trying to await it.
            return product;
        }

        public async Task<int> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            return id;
        }
    }
}
