using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PalmyraHospital.Application.Interfaces.Admin;

namespace PalmyraHospital.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IAdminDashboardService _dashboardService;

    public AdminController(IAdminDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _dashboardService.GetDashboardAsync();
        return View(model);
    }
}