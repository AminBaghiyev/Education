using Education.BL.DTOs;
using Education.BL.Services.Abstractions;
using Education.Core.Models;
using Education.DL.Repository.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Education.PL.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class NewsController : Controller
{
    readonly INewsService _service;
    readonly IRepository<Category> _categoryRepository;

    public NewsController(INewsService service, IRepository<Category> categoryRepository)
    {
        _service = service;
        _categoryRepository = categoryRepository;
    }

    public async Task<IActionResult> Index(int page = 0, string? search = null)
    {
        IEnumerable<NewsListItemDTO> list = await _service.GetAllListItemsAsync(search, page);
        ViewData["Page"] = page;
        ViewData["TotalCount"] = search is null ? _service.TotalCount : await _service.TotalSearchCountAsync(search);
        ViewData["Search"] = search;

        return View(list);
    }

    public async Task<IActionResult> Create()
    {
        ViewData["Categories"] = new SelectList(await _categoryRepository.GetAllAsync(), nameof(Category.Id), nameof(Category.Title));
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NewsCreateDTO dto)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Categories"] = new SelectList(await _categoryRepository.GetAllAsync(), nameof(Category.Id), nameof(Category.Title));
            return View(dto);
        }

        try
        {
            await _service.CreateAsync(dto, User.Identity.Name);
            await _service.SaveChangesAsync();
        }
        catch (Exception)
        {
            ViewData["Categories"] = new SelectList(await _categoryRepository.GetAllAsync(), nameof(Category.Id), nameof(Category.Title));
            ModelState.AddModelError("CustomError", "Something went wrong!");
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        try
        {
            ViewData["Categories"] = new SelectList(await _categoryRepository.GetAllAsync(), nameof(Category.Id), nameof(Category.Title));
            return View(await _service.GetByIdForUpdateAsync(id));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(NewsUpdateDTO dto)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Categories"] = new SelectList(await _categoryRepository.GetAllAsync(), nameof(Category.Id), nameof(Category.Title));
            return View(dto);
        }

        try
        {
            await _service.UpdateAsync(dto, User.Identity.Name);
            await _service.SaveChangesAsync();
        }
        catch (Exception)
        {
            ViewData["Categories"] = new SelectList(await _categoryRepository.GetAllAsync(), nameof(Category.Id), nameof(Category.Title));
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
        News news;
        try
        {
            news = await _service.GetByIdAsync(id);
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return View(news);
    }
}
