using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PalmyraHospital.Application.DTOs.Admin;
using PalmyraHospital.Infrastructure.Data;
using PalmyraHospital.Domain.Entities;
using PalmyraHospital.Infrastructure.Identity;
using PalmyraHospital.Web.ViewModels.Admin;

namespace PalmyraHospital.Web.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // ================= DASHBOARD =================
        public async Task<IActionResult> Index()
        {
            var model = new AdminDashboardViewModel
            {
                TotalDoctors = await _context.Doctors.CountAsync(d => !d.IsArchived),
                ArchivedDoctors = await _context.Doctors.CountAsync(d => d.IsArchived),
                TotalPatients = await _context.Patients.CountAsync(),
                TotalDepartments = await _context.Departments.CountAsync(),
                TodayAppointments = await _context.Appointments
                    .CountAsync(a => a.AppointmentDate.Date == DateTime.Today)
            };

            return View(model);
        }

        // ================= DOCTORS =================
        public async Task<IActionResult> Doctors()
        {
            var doctors = await _context.Doctors
                .Include(d => d.Department)
                .ToListAsync();

            return View(doctors);
        }

        // ================= PATIENTS =================
        public async Task<IActionResult> Patients()
        {
            var patients = await _context.Patients
                .ToListAsync();

            return View(patients);
        }

        // ================= DEPARTMENTS =================
        public async Task<IActionResult> Departments()
        {
            var departments = await _context.Departments
                .ToListAsync();

            return View(departments);
        }

        // ================= APPOINTMENTS =================
        public async Task<IActionResult> Appointments()
        {
            var appointments = await _context.Appointments
     .Include(a => a.Patient)
     .Include(a => a.Doctor)
     .Include(a => a.Doctor.Department)
     .Include(a => a.Doctor.Specialization)
     .ToListAsync();

            return View(appointments);
        }
    }
}
