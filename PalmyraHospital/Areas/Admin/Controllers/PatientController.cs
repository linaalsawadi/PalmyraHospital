using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PalmyraHospital.Application.Interfaces;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class PatientController : Controller
{
    private readonly IPatientService _service;

    public PatientController(IPatientService service)
    {
        _service = service;
    }

    // ================= INDEX =================
    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        return View(data);
    }

    // ================= CREATE =================
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreatePatientViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        try
        {
            await _service.CreateAsync(
                vm.FirstName,
                vm.LastName,
                vm.DateOfBirth,
                vm.Gender,
                vm.NationalId,
                vm.PhoneNumber,
                vm.Address
            );

            TempData["Success"] = "Patient created successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Something went wrong");
            return View(vm);
        }
    }

    // ================= EDIT =================
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var p = await _service.GetByIdAsync(id);
        if (p == null) return NotFound();

        var vm = new EditPatientViewModel
        {
            Id = p.Id,
            FirstName = p.FullName.Split(' ')[0],
            LastName = p.FullName.Split(' ').Last(),
            PhoneNumber = p.Phone
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditPatientViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        try
        {
            await _service.UpdateAsync(vm.Id, vm.PhoneNumber, vm.Address);

            TempData["Success"] = "Patient updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            ModelState.AddModelError("", "Something went wrong");
            return View(vm);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            TempData["Success"] = "Patient archived successfully";
        }
        catch (PatientNotFoundException ex)
        {
            TempData["Error"] = ex.Message;
        }
        catch (Exception)
        {
            TempData["Error"] = "Something went wrong";
        }

        return RedirectToAction(nameof(Index));
    }
}