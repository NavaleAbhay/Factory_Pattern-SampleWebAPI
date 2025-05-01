using SampleWebAPI.Models;
using SampleWebAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SampleWebAPI.Repositories
{
    public class FileProductRepository : IProductRepository
    {
        private readonly string _filePath = @"d:/tryout/credential.json";

        public async Task<IEnumerable<Product>> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Product>();
            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public async Task<Product> GetById(int id)
        {
            var products = await GetAll();
            return products.FirstOrDefault(p => p.ProductID == id);
        }

        public async Task<Product> Add(Product product)
        {
            var products = (await GetAll()).ToList();
            products.Add(product);
            await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(products));
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            var products = (await GetAll()).ToList();
            var index = products.FindIndex(p => p.ProductID == product.ProductID);
            if (index != -1)
            {
                products[index] = product;
                await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(products));
            }
            return product;
        }

        public async Task<int> Delete(int id)
        {
            var products = (await GetAll()).ToList();
            var removedCount = products.RemoveAll(p => p.ProductID == id);
            await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(products));
            return removedCount;
        }
    }
}
