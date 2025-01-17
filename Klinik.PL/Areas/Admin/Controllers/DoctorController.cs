using Klinik.BL.DTOs;
using Klinik.BL.Exceptions;
using Klinik.BL.Services.Abstractions;
using Klinik.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Klinik.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DoctorController : Controller
{
    readonly IDoctorService _service;
    readonly IDepartmentService _departmentService;

    public DoctorController(IDoctorService service, IDepartmentService departmentService)
    {
        _service = service;
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<DoctorListItemDTO> list = await _service.GetAllListItemsAsync();

        return View(list);
    }

    public async Task<IActionResult> Create()
    {
        ViewData["Departments"] = new SelectList(await _departmentService.GetAllListItemsAsync(), nameof(Department.Id), nameof(Department.Title));

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DoctorCreateDTO dto)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Departments"] = new SelectList(await _departmentService.GetAllListItemsAsync(), nameof(Department.Id), nameof(Department.Title));
            return View(dto);
        }

        try
        {
            await _service.CreateAsync(dto, User.Identity.Name);
            await _service.SaveChangesAsync();
        }
        catch (BaseException e)
        {
            ViewData["Departments"] = new SelectList(await _departmentService.GetAllListItemsAsync(), nameof(Department.Id), nameof(Department.Title));
            ModelState.AddModelError("CustomError", e.Message);
            return View(dto);
        }
        catch (Exception)
        {
            ViewData["Departments"] = new SelectList(await _departmentService.GetAllListItemsAsync(), nameof(Department.Id), nameof(Department.Title));
            ModelState.AddModelError("CustomError", "Something went wrong!");
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        try
        {
            ViewData["Departments"] = new SelectList(await _departmentService.GetAllListItemsAsync(), nameof(Department.Id), nameof(Department.Title));
            return View(await _service.GetByIdForUpdateAsync(id));
        }
        catch (BaseException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("Something went wrong!");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(DoctorUpdateDTO dto)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Departments"] = new SelectList(await _departmentService.GetAllListItemsAsync(), nameof(Department.Id), nameof(Department.Title));
            return View(dto);
        }

        try
        {
            await _service.UpdateAsync(dto, User.Identity.Name);
            await _service.SaveChangesAsync();
        }
        catch (BaseException e)
        {
            ViewData["Departments"] = new SelectList(await _departmentService.GetAllListItemsAsync(), nameof(Department.Id), nameof(Department.Title));
            ModelState.AddModelError("CustomError", e.Message);
            return View(dto);
        }
        catch (Exception)
        {
            ViewData["Departments"] = new SelectList(await _departmentService.GetAllListItemsAsync(), nameof(Department.Id), nameof(Department.Title));
            ModelState.AddModelError("CustomError", "Something went wrong!");
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (BaseException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("Something went wrong!");
        }
    }
}
