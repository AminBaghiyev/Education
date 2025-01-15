using Education.BL.DTOs;
using Education.BL.Services.Abstractions;
using Education.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Education.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(int page = 0, string? search = null)
    {
        IEnumerable<CategoryListItemDTO> list = await _service.GetAllListItemsAsync(search, page);
        ViewData["Page"] = page;
        ViewData["TotalCount"] = search is null ? _service.TotalCount : await _service.TotalSearchCountAsync(search);
        ViewData["Search"] = search;

        return View(list);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryCreateDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            await _service.CreateAsync(dto, User.Identity.Name);
            await _service.SaveChangesAsync();
        }
        catch (Exception)
        {
            ModelState.AddModelError("CustomError", "Something went wrong!");
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        try
        {
            return View(await _service.GetByIdForUpdateAsync(id));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(CategoryUpdateDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            await _service.UpdateAsync(dto, User.Identity.Name);
            await _service.SaveChangesAsync();
        }
        catch (Exception)
        {
            ModelState.AddModelError("CustomError", "Something went wrong!");
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        Category category;
        try
        {
            category = await _service.GetByIdAsync(id);
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return View(category);
    }
}
