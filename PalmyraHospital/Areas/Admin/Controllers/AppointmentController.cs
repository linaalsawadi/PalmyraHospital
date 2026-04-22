using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PalmyraHospital.Infrastructure.Data;

namespace PalmyraHospital.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AppointmentController : Controller
{
    private readonly ApplicationDbContext _context;

    public AppointmentController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.Doctor.Department)
            .Include(a => a.Doctor.Specialization)
            .ToListAsync();

        return View(data);
    }
}