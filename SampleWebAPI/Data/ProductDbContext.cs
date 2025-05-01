using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebAPI.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
  