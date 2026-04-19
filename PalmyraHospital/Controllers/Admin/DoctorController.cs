using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PalmyraHospital.Infrastructure.Data;
using PalmyraHospital.Infrastructure.Identity;
using PalmyraHospital.Domain.Entities;
using PalmyraHospital.Web.ViewModels.Admin;

namespace PalmyraHospital.Web.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DoctorController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // ================================
        // LIST DOCTORS
        // ================================
        public async Task<IActionResult> Index()
        {
            var doctors = await _context.Doctors
                .Include(d => d.Department)
                .Include(d => d.Specialization)
                .Where(d => !d.IsArchived)
                .ToListAsync();

            return View("~/Views/Admin/Doctor/Doctors.cshtml", doctors);
        }

        // ================================
        // CREATE DOCTOR (GET)
        // ================================
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CreateDoctorViewModel
            {
                Departments = await _context.Departments
                    .OrderBy(d => d.Name)
                    .Select(d => new ValueTuple<int, string>(d.Id, d.Name))
                    .ToListAsync(),

                Specializations = await _context.Specializations
                    .OrderBy(s => s.Name)
                    .Select(s => new ValueTuple<int, string>(s.Id, s.Name))
                    .ToListAsync()
            };

            return View("~/Views/Admin/Doctor/CreateDoctor.cshtml", vm);
        }

        // ================================
        // CREATE DOCTOR (POST)
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDoctorViewModel vm)
        {
            vm.Departments = await _context.Departments
                .Select(d => new ValueTuple<int, string>(d.Id, d.Name))
                .ToListAsync();

            vm.Specializations = await _context.Specializations
                .Select(s => new ValueTuple<int, string>(s.Id, s.Name))
                .ToListAsync();

            if (!ModelState.IsValid)
                return View("~/Views/Admin/Doctor/CreateDoctor.cshtml", vm);

            // Role check
            if (!await _roleManager.RoleExistsAsync("Doctor"))
                await _roleManager.CreateAsync(new IdentityRole("Doctor"));

            // Email exists check
            if (await _userManager.FindByEmailAsync(vm.Email) != null)
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View("~/Views/Admin/Doctor/CreateDoctor.cshtml", vm);
            }

            await using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                // Create Identity User
                var user = new ApplicationUser
                {
                    UserName = vm.Email,
                    Email = vm.Email,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    PhoneNumber = vm.PhoneNumber,
                    IsActive = true,
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, vm.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);

                    return View("~/Views/Admin/Doctor/CreateDoctor.cshtml", vm);
                }

                await _userManager.AddToRoleAsync(user, "Doctor");

                // Generate DoctorNumber
                var count = await _context.Doctors.CountAsync();
                var doctorNumber = $"DOC-{(count + 1):00000}";

                // Create Doctor Entity
                var doctor = new Doctor(
                    userId: user.Id,
                    doctorNumber: doctorNumber,
                    licenseNumber: vm.LicenseNumber,
                    firstName: vm.FirstName,
                    lastName: vm.LastName,
                    phoneNumber: vm.PhoneNumber,
                    departmentId: vm.DepartmentId,
                    specializationId: vm.SpecializationId,
                    yearsOfExperience: vm.YearsOfExperience,
                    consultationFee: vm.ConsultationFee
                );

                _context.Doctors.Add(doctor);

                await _context.SaveChangesAsync();

                await tx.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await tx.RollbackAsync();

                ModelState.AddModelError("", "Error creating doctor");

                return View("~/Views/Admin/Doctor/CreateDoctor.cshtml", vm);
            }
        }

        // ================================
        // ARCHIVE DOCTOR
        // ================================
        [HttpPost]
        public async Task<IActionResult> Archive(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
                return NotFound();

            doctor.Archive();

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
