using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PalmyraHospital.Domain.Entities;
using PalmyraHospital.Infrastructure.Data;

namespace PalmyraHospital.Web.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments.ToListAsync();
            return View("~/Views/Admin/Department/Departments.cshtml", departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Departments.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }

}
