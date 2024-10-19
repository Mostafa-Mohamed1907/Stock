using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock.Container;
using Stock.Models;

namespace Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly PointOfSaleContext context;
        public CategoryController(PointOfSaleContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult <List<Category>> GetAll()
        {
            return context.category.Include(p=>p.products).ToList();
        }
        [HttpGet("{id}")]
        public ActionResult <Category> GetById(int id)
        {
            var category = context.category.Include(p => p.products).FirstOrDefault(p => p.Id == id);
            if (category == null) { return NotFound(); }
            return category;
        }
        [HttpPost]
        public ActionResult <Category> CreateCategory([FromBody] Category category)
        {
            context.category.Add(category);
            context.SaveChanges();
            return category;
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            var ExistingCategroy = context.category.Include(p => p.products).FirstOrDefault(p => p.Id == id);
            if(ExistingCategroy == null) { return NotFound(); }
            ExistingCategroy.Id = category.Id;
            ExistingCategroy.Name = category.Name;
            context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var ExistingCategroy = context.category.Include(p => p.products).FirstOrDefault(p => p.Id == id);
            if (ExistingCategroy == null) { return NotFound(); }
            context.category.Remove(ExistingCategroy);
            context.SaveChanges();
            return Ok();
        }


    }
}
