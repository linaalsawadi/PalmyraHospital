using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PalmyraHospital.Application.Interfaces.Admin;
using PalmyraHospital.Web.ViewModels.Admin.Doctor;

namespace PalmyraHospital.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DoctorController : Controller
{
    private readonly IDoctorService _service;
    private readonly ILookupService _lookupService;
    private async Task LoadDropdowns(EditDoctorViewModel vm)
    {
        vm.Departments = await _lookupService.GetDepartmentsAsync();
        vm.Specializations = await _lookupService.GetSpecializationsAsync();
    }
    private async Task LoadDropdowns(CreateDoctorViewModel vm)
    {
        vm.Departments = await _lookupService.GetDepartmentsAsync();
        vm.Specializations = await _lookupService.GetSpecializationsAsync();
    }
    public DoctorController(
    IDoctorService service,
    ILookupService lookupService)
    {
        _service = service;
        _lookupService = lookupService;
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
    public async Task<IActionResult> Create()
    {
        var vm = new CreateDoctorViewModel
        {
            Departments = await _lookupService.GetDepartmentsAsync()
        };

        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateDoctorViewModel vm)
    {
        // =========================
        // 1. Validation
        // =========================
        if (!ModelState.IsValid)
        {
            await LoadDropdowns(vm);
            return View(vm);
        }

        try
        {
            // =========================
            // 2. Call Service
            // =========================
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
        catch (DuplicateDoctorException ex)
        {
            ModelState.AddModelError("LicenseNumber", ex.Message);
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Something went wrong");
        }

        await LoadDropdowns(vm);
        return View(vm);
    }

    // =========================
    // EDIT
    // =========================

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _service.GetForEditAsync(id);

        var vm = new EditDoctorViewModel
        {
            Id = dto.Id,
            PhoneNumber = dto.Phone,
            YearsOfExperience = dto.YearsOfExperience,
            ConsultationFee = dto.ConsultationFee,
            FirstName = dto.FullName.Split(' ')[0],
            LastName = dto.FullName.Split(' ').Length > 1 ? dto.FullName.Split(' ')[1] : "",

            //Dropdowns
            Departments = await _lookupService.GetDepartmentsAsync(),
            Specializations = await _lookupService.GetSpecializationsAsync()
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditDoctorViewModel vm)
    {
        // =========================
        // 1. Validation
        // =========================
        if (!ModelState.IsValid)
        {
            await LoadDropdowns(vm);
            return View(vm);
        }

        try
        {
            await _service.UpdateAsync(
                vm.Id,
                vm.FirstName,
                vm.LastName,
                vm.PhoneNumber,
                vm.DepartmentId,
                vm.SpecializationId,
                vm.YearsOfExperience,
                vm.ConsultationFee
            );

            TempData["Success"] = "Doctor updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (DoctorNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Something went wrong");

            await LoadDropdowns(vm);
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
        catch (DoctorNotFoundException ex)
        {
            TempData["Error"] = ex.Message;
        }
        catch (Exception)
        {
            TempData["Error"] = "Something went wrong";
        }

        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> GetSpecializations(int departmentId)
    {
        var data = await _lookupService.GetSpecializationsByDepartmentAsync(departmentId);

        return Json(data.Select(s => new
        {
            id = s.Id,
            name = s.Name
        }));
    }
}