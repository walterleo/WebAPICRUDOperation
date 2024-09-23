using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    AppDbContext _db;
    public ProductController(AppDbContext db)
    {
      _db = db;
    }

    // GET: api/product
    [HttpGet]
    public IEnumerable<Product> GetAll()
    {
      return _db.Products.ToList();
      
    }

    // GET: api/product
    [HttpGet]
    public IEnumerable<Product> GetProduct()
    {
      return _db.Products.Include(p=>p.Category).ToList();

    }

    // GET: api/product/{id}
    [HttpGet("{id}")]
    public Product Get(int id)
    {      
        return _db.Products.Find(id);     

    }




  }
}
