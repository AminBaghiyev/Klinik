using Microsoft.AspNetCore.Mvc;

namespace Klinik.PL.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
