using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock.Container;
using Stock.Models;

namespace Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly PointOfSaleContext context;
        public ProductController(PointOfSaleContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return context.products.Include(p => p.category).ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = context.products.Include(p => p.category).FirstOrDefault(p => p.Id == id);
            if (product == null) { return NotFound(); }
            return product;
        }
        [HttpPost]
        public ActionResult <Product> CreateProduct([FromBody] Product product)
        {
            context.products.Add(product);
            context.SaveChanges();
            return product;
        }
    }
}
