using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}