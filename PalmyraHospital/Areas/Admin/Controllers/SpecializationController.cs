using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PalmyraHospital.Application.Interfaces.Admin;

namespace PalmyraHospital.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SpecializationController : Controller
{
    private readonly ISpecializationService _service;
    private readonly ILookupService _lookup;

    public SpecializationController(
        ISpecializationService service,
        ILookupService lookup)
    {
        _service = service;
        _lookup = lookup;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        return View(data);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Departments = await _lookup.GetDepartmentsAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name, int departmentId)
    {
        await _service.CreateAsync(name, departmentId);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var spec = await _service.GetByIdAsync(id);
        ViewBag.Departments = await _lookup.GetDepartmentsAsync();
        return View(spec);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string name, int departmentId)
    {
        await _service.UpdateAsync(id, name, departmentId);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}