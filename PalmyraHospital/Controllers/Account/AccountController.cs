using Microsoft.AspNetCore.Mvc;
using PalmyraHospital.Application.Interfaces.Auth;
using PalmyraHospital.Application.DTOs.Auth;
using PalmyraHospital.Web.ViewModels.Account;
using PalmyraHospital.Application.Implementations;
using PalmyraHospital.Application.Interfaces.Redirect;
using System.Security.Claims;

public class AccountController : Controller
{
    private readonly IAuthService _authService;
    private readonly IUserRedirectService _redirectService;

    public AccountController(IAuthService authService,
        IUserRedirectService redirectService)
    {
        _authService = authService;
        _redirectService = redirectService;

    }

    // ================= LOGIN =================

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var request = new LoginRequest
        {
            Email = model.Email,
            Password = model.Password,
            RememberMe = model.RememberMe
        };

        var result = await _authService.LoginAsync(request);

        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Login failed.");
            return View(model);
        }

        //Clean Role-Based Redirect (SOLID)
        var redirect = _redirectService.GetRedirect(User);

        return RedirectToAction(
            actionName: redirect.action,
            controllerName: redirect.controller);
    }



    // ================= REGISTER =================

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var request = new RegisterRequest
        {
            Name = model.Name,
            Email = model.Email,
            Password = model.Password
        };

        var result = await _authService.RegisterAsync(request);

        if (result.Success)
            return RedirectToAction("Login");

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error);

        return View(model);
    }

    // ================= VERIFY EMAIL =================

    [HttpGet]
    public IActionResult VerifyEmail()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _authService.VerifyEmailAsync(model.Email);

        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, "User not found!");
            return View(model);
        }

        return RedirectToAction("ChangePassword", new { username = result.Username });
    }

    // ================= CHANGE PASSWORD =================

    [HttpGet]
    public IActionResult ChangePassword(string username)
    {
        if (string.IsNullOrEmpty(username))
            return RedirectToAction("VerifyEmail");

        return View(new ChangePasswordViewModel { Email = username });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var request = new ChangePasswordRequest
        {
            Email = model.Email,
            NewPassword = model.NewPassword
        };

        var result = await _authService.ChangePasswordAsync(request);

        if (result.Success)
            return RedirectToAction("Login");

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error);

        return View(model);
    }

    // ================= LOGOUT =================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction("Login", "Account");
    }
}
