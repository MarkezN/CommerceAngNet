using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // GET
    [HttpGet]
    public string GetProducts()
    {
        return "Get all products";
    }
    
    [HttpGet("{id}")]
    public string GetProduct(int id)
    {
        return "Get single product";
    }
}