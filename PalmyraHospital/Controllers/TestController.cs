using Microsoft.AspNetCore.Mvc;
using PalmyraHospital.Infrastructure.Logging.Abstractions;
using PalmyraHospital.Infrastructure.Logging.Enums;

namespace PalmyraHospital.Web.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly ILogService _logService;

    private readonly ISecurityLogger _securityLogger;

    public TestController(
        ILogService logService,
        ISecurityLogger securityLogger)
    {
        _logService = logService;
        _securityLogger = securityLogger;
    }

    [HttpGet]
    public IActionResult Test()
    {
        _logService.WriteLog(
            "Application logging test successful",
            LogSeverity.Information);

        return Ok("Normal logging works");
    }

    [HttpGet("security")]
    public async Task<IActionResult> SecurityTest()
    {
        await _securityLogger.LogSecurityEventAsync(
            "Failed login attempt detected",
            SecurityEventType.LoginFailed,
            LogSeverity.Warning,
            userId: "patient-001",
            ipAddress: "192.168.1.10");

        return Ok("Security logging works");
    }

    [HttpGet("crash")]
    public IActionResult Crash()
    {
        throw new Exception(
            "Test exception from API");
    }
}