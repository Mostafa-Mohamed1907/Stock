using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock.Container;
using Stock.Dtos;
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
        public ActionResult<List<ProductDto>> GetAll()
        {
            var products = context.products.Include(p => p.category).ToList();
            //Map each product to productDto
            var ProductDto = products.Select(p=> new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryName = p.category.Name

            }).ToList();
            return ProductDto;
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            var product = context.products.Include(p => p.category).FirstOrDefault(p => p.Id == id);
            if (product == null) { return NotFound(); }
            var productDto = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryName = product.category.Name
            };
            return productDto;
        }
        [HttpGet("v2/{id}")]
        [ApiVersion("2.0")]
        public ActionResult<ProductDto> GetByIdV2(int id)
        {
            var product = context.products.Include(p => p.category).FirstOrDefault(p => p.Id == id);
            if (product == null) { return NotFound(); }
            var productDto = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryName = product.category.Name
            };
            return productDto;
        }
        [HttpPost]
        public ActionResult  CreateProduct([FromBody] ProductCreateDto product)
        {
            if (!ModelState.IsValid) { BadRequest(ModelState); }
            var newProduct = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
            context.products.Add(newProduct);
            context.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] ProductUpdateDto product)
        {
            var ExistingProduct = context.products.Include(p => p.category).FirstOrDefault(p => p.Id == id);
            if (ExistingProduct == null) { return NotFound(); }
            ExistingProduct.Name = product.Name;
            ExistingProduct.Description = product.Description;
            ExistingProduct.Price = product.Price;
            ExistingProduct.CategoryId = product.CategoryId;
           
            context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var ExistingProduct = context.products.Include(p => p.category).FirstOrDefault(p => p.Id == id);
            if (ExistingProduct == null) { return NotFound(); }
            context.products.Remove(ExistingProduct);
            context.SaveChanges();
            return Ok();
        }







        //[HttpGet]
        //public ActionResult<List<Product>> GetAll()
        //{
        //    return context.products.Include(p => p.category).ToList();
        //}
        //[HttpGet("{id}")]
        //public ActionResult<Product> GetById(int id)
        //{
        //    var product = context.products.Include(p => p.category).FirstOrDefault(p => p.Id == id);
        //    if (product == null) { return NotFound(); }
        //    return product;
        //}
        //[HttpPost]
        //public ActionResult <Product> CreateProduct([FromBody] Product product)
        //{
        //    context.products.Add(product);
        //    context.SaveChanges();
        //    return product;
        //}
    }
}
