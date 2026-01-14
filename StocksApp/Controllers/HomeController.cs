using Microsoft.AspNetCore.Mvc;
using StocksApp.Services;

namespace StocksApp.Controllers;

public class HomeController : Controller
{
    private readonly MyService _myService;

    public HomeController(MyService myService)
    {
        _myService = myService;
    }
    // GET
    [Route("/")]
    public async Task<IActionResult> Index()
    {
        var response = await _myService.Method();
        return Ok(response);
    }
}