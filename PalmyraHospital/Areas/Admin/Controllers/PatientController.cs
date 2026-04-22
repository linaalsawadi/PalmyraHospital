using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PalmyraHospital.Application.Interfaces;

namespace PalmyraHospital.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class PatientController : Controller
{
    private readonly IPatientService _service;

    public PatientController(IPatientService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var patients = await _service.GetAllAsync();
        return View(patients);
    }
}