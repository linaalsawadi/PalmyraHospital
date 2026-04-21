using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PalmyraHospital.Application.Interfaces.Admin;
using PalmyraHospital.Web.ViewModels.Admin.Department;

namespace PalmyraHospital.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DepartmentController : Controller
{
    private readonly IDepartmentService _service;

    public DepartmentController(IDepartmentService service)
    {
        _service = service;
    }

    // =========================
    // INDEX
    // =========================
    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        return View(data); // يبحث عن Index.cshtml تلقائياً ✔
    }

    // =========================
    // CREATE (GET)
    // =========================
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // =========================
    // CREATE (POST)
    // =========================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateDepartmentViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _service.CreateAsync(model.Name, model.Description);

            // ⭐ رسالة نجاح (اختياري)
            TempData["Success"] = "Department created successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }

    // =========================
    // EDIT (GET)
    // =========================
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _service.GetForEditAsync(id);

        if (dto == null)
            return NotFound();

        var vm = new EditDepartmentViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description
        };

        return View(vm);
    }

    // =========================
    // EDIT (POST)
    // =========================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditDepartmentViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _service.UpdateAsync(model.Id, model.Name, model.Description);

            TempData["Success"] = "Department updated successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }

    // =========================
    // DELETE
    // =========================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);

            TempData["Success"] = "Department deleted successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}