using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class Controller1 : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}