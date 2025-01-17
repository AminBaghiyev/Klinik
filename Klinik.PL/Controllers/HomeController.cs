using Klinik.BL.Services.Abstractions;
using Klinik.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Klinik.PL.Controllers;

public class HomeController : Controller
{
    readonly IDoctorService _doctorService;

    public HomeController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    public async Task<IActionResult> Index()
    {
        HomeVM VM = new()
        {
            Doctors = await _doctorService.GetAllViewItemsAsync(4)
        };

        return View(VM);
    }
}
