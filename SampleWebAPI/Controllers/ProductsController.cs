using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data;
using SampleWebAPI.Models;
using SampleWebAPI.Repositories;
using SampleWebAPI.Repositories.Interfaces;
using System.Collections.Generic;

namespace SampleWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext _context;
        private readonly string _connectionString;  

        public ProductsController(ProductDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString ("DefaultConnection");
        }

        private IProductRepository GetRepository(string repositoryType)
        {
            return ProductRepositoryFactory.Create(repositoryType, _connectionString, _context);
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Product>> GetAll([FromQuery] string repositoryType = "EntityFramework")
        {
            var repository = GetRepository(repositoryType);
            return await repository.GetAll();
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Product>> GetById(int id, [FromQuery] string repositoryType = "EntityFramework")
        {
            var repository = GetRepository(repositoryType);
            var product = await repository.GetById(id);
            if (product == null) return NotFound();
            return product;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Product product, [FromQuery] string repositoryType = "EntityFramework")
        {
            var repository = GetRepository(repositoryType);
            await repository.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.ProductID, repositoryType }, product);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, Product product, [FromQuery] string repositoryType = "EntityFramework")
        {
            if (id != product.ProductID) return BadRequest();
            var repository = GetRepository(repositoryType);
            await repository.Update(product);
            return NoContent();
        }

        [HttpDelete("Delete")]
        public async  Task<IActionResult> Delete(int id, [FromQuery] string repositoryType = "EntityFramework")
        {
            var repository = GetRepository(repositoryType);
            await repository.Delete(id);  
            return NoContent();
        }
    }
}
