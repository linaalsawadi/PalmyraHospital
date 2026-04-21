using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PalmyraHospital.Application.Interfaces.Admin;
using PalmyraHospital.Web.ViewModels.Admin;

namespace PalmyraHospital.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DoctorController : Controller
{
    private readonly IDoctorService _service;

    public DoctorController(IDoctorService service)
    {
        _service = service;
    }

    // =========================
    // INDEX
    // =========================
    public async Task<IActionResult> Index()
    {
        var doctors = await _service.GetAllAsync();
        return View(doctors);
    }

    // =========================
    // CREATE (GET)
    // =========================
    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateDoctorViewModel());
    }

    // =========================
    // CREATE (POST)
    // =========================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateDoctorViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        try
        {
            await _service.CreateAsync(
                vm.Email,
                vm.Password,
                vm.FirstName,
                vm.LastName,
                vm.LicenseNumber,
                vm.PhoneNumber,
                vm.DepartmentId,
                vm.SpecializationId,
                vm.YearsOfExperience,
                vm.ConsultationFee
            );

            TempData["Success"] = "Doctor created successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(vm);
        }
    }

    // =========================
    // DELETE (ARCHIVE)
    // =========================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            TempData["Success"] = "Doctor archived successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}