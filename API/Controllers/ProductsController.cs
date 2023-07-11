using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly StoreContext _context;
    public ProductsController(StoreContext context)
    {
        _context = context;
    }
    // GET
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        //return "Get all products";
        var products = await _context.Products.ToListAsync();
        return Ok(products);

    }
    
    [HttpGet("{id}")]
    public string GetProduct(int id)
    {
        return "Get single product";
    }
}