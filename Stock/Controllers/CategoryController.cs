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

    }
}
