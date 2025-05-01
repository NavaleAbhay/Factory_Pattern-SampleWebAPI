using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Data;
using SampleWebAPI.Models;
using SampleWebAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SampleWebAPI.Repositories
{
    public class EfProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public EfProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync(); 
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> Add(Product product)
        {
            var entity = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Product> Update(Product product)
        {
            var entity = _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
