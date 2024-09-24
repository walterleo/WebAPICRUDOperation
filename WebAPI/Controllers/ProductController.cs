using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    AppDbContext _db;
    public ProductController(AppDbContext db)
    {
      _db = db;
    }

    // GET: api/product/getall
    [HttpGet]
    public IEnumerable<Product> GetAll()
    {
      return _db.Products.ToList(); // data +dataType
      
    }

    [HttpGet]
    public IActionResult GetList()
    {
      var products = _db.Products.ToList();  // data + custom status code
      return Ok(products);

    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProduct()
    {
      var products = _db.Products.ToList();  // data + custom status code
      return Ok(products);

    }

    // GET: api/product/getproduct
    [HttpGet]
    public IEnumerable<Product> GetProducts()
    {
      return _db.Products.Include(p=>p.Category).ToList();

    }

    // GET: api/product/get/{id}
    [HttpGet("{id}")]
    public Product Get(int id)
    {      
        return _db.Products.Find(id);     

    }


    [HttpPost]
    public IActionResult Add(Product product)
    {
      try
      {
        _db.Products.Add(product);
        _db.SaveChanges();
        return CreatedAtAction("Get", product); /// 201

      }
      catch (Exception ex)
      {

        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); // 500
      }

    }



    [HttpPut("{id}")]
    public IActionResult Update([FromQuery]int id, [FromBody] Product product)
    {
      try
      {
        if(id != product.ProductId)
        { 
          return BadRequest();
        
        }

        _db.Products.Update(product);
        _db.SaveChanges();
        return Ok(product); /// 201

      }
      catch (Exception ex)
      {

        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); // 500
      }

    }

    [HttpPatch("{id}/{price}")]
    public IActionResult PatchProductPrice(int id, string price)
    {
      try
      {
        var product = _db.Products.Find(id);
        if (product == null)
        {
          return NotFound();

        }

        product.Price = price;
        _db.SaveChanges();
        return Ok(product); /// 200

      }
      catch (Exception ex)
      {

        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); // 500
      }

    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {

      try
      {
        var product = _db.Products.Find(id);
        if (product == null)
        {
          return NotFound();

        }

        _db.Products.Remove(product);
        _db.SaveChanges();
        return Ok(); /// 201

      }
      catch (Exception ex)
      {

        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); // 500
      }

    }
        



  }
}
