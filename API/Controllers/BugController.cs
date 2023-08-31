using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BugController : BaseApiController
{
    private readonly StoreContext _context;
    
    public BugController(StoreContext context)
    {
        _context = context;
    }
    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
        var product = _context.Products.Find(50);
        return product == null ? NotFound(new ApiResponse(404)) : Ok(); 
    }
    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
        var product = _context.Products.Find(50);
        var prodToReturn = product.ToString();
        return Ok();
    }
    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    { 
        return BadRequest(new ApiResponse(400));
    }
    [HttpGet("badrequest/{id}")]
    public ActionResult GetNotFoundRequest(int id)
    {
        return Ok();
    }
}