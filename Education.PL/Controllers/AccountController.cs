using Education.BL.DTOs;
using Education.BL.Services.Abstractions;
using Education.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Education.PL.Controllers;

public class AccountController : Controller
{
    readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }

    public IActionResult Login()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return Redirect(User.IsInRole(UserRoles.Admin.ToString()) ? "/admin" : "/");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            await _service.LoginAsync(dto);
        }
        catch (Exception)
        {
            ModelState.AddModelError("CustomError", "Credentials are wrong!");
            return View(dto);
        }

        return Redirect(User.IsInRole(UserRoles.Admin.ToString()) ? "/admin" : "/");
    }

    public IActionResult Register()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return Redirect(User.IsInRole(UserRoles.Admin.ToString()) ? "/admin" : "/");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegisterDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            await _service.RegisterAsync(dto);
        }
        catch (Exception)
        {
            ModelState.AddModelError("CustomError", "Something went wrong!");
            return View(dto);
        }

        return Redirect("/account/login");
    }

    public async Task<IActionResult> Logout()
    {
        try
        {
            await _service.LogoutAsync();
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return Redirect("/");
    }
}
