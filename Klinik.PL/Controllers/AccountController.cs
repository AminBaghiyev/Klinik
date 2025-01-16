using Klinik.BL.DTOs;
using Klinik.BL.Services.Abstractions;
using Klinik.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Klinik.PL.Controllers;

public class AccountController : Controller
{
    readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }

    public IActionResult Login()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return Redirect(User.IsInRole(Roles.Admin.ToString()) ? "/admin" : "/");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginDTO dto, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            await _service.LoginAsync(dto);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("CustomError", ex.Message);
            return View(dto);
        }

        return Redirect(returnUrl ?? (User.IsInRole(Roles.Admin.ToString()) ? "/admin" : "/"));
    }

    public IActionResult Register()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return Redirect(User.IsInRole(Roles.Admin.ToString()) ? "/admin" : "/");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegisterDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            await _service.RegisterAsync(dto);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("CustomError", ex.Message);
            return View(dto);
        }

        return RedirectToAction(nameof(Login));
    }

    public async Task<IActionResult> Logout()
    {
        await _service.LogoutAsync();

        return Redirect("/");
    }
}
