using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InteractiveGallery.Infrastructure.Data;
using InteractiveGallery.Core.ArtistAggregate;
using InteractiveGallery.SharedKernel.Interfaces;
using InteractiveGallery.Core.CategoryAggregate;
using InteractiveGallery.Core.CategoryAggregate.Specifications;
using InteractiveGallery.Core.ArtistAggregate.Specifications;
using Microsoft.AspNetCore.Authorization;

namespace InteractiveGallery.Web.Areas.AdminArea.Controllers;
[Area("AdminArea")]
[Route("AdminArea/[controller]")]
[Authorize(Roles = "Admin")]
public class CategoriesController : Controller
{
  private readonly IRepository<Category> _categoryRepository;


  public CategoriesController(IRepository<Category> categoryRepository)
  {
    _categoryRepository = categoryRepository;
  }

  // GET: Categories
  [HttpGet]
  public async Task<IActionResult> Index()
  {
    var categories = await _categoryRepository.ListAsync();
    return View(categories.ToArray());
  }
  // GET: Categories/Details/5
  [HttpGet("details/{id:int}")]
  public async Task<IActionResult> Details(int id)
  {
    var spec = new CategoryByIdSpec(id);
    var category = await _categoryRepository.FirstOrDefaultAsync(spec);
    if (category == null)
    {
      return NotFound();
    }

    var categoryVO = new CategoryValueObject
    {
      Id = category.Id,
      Name = category.Name,
      Description = category.Description,
    };
    return View(categoryVO);
  }

  // GET: Categories/Create
  [HttpGet("create")]
  public IActionResult Create()
  {
    return View();
  }

  // POST: Categories/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost("create")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(CategoryValueObject categoryValueObject)
  {
    var category = new Category(categoryValueObject);
    if (ModelState.IsValid)
    {
      await _categoryRepository.AddAsync(category);
      await _categoryRepository.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(category);
  }

  // GET: Categories/Edit/5
  [HttpGet("edit/{id:int}")]
  public async Task<IActionResult> Edit(int id)
  {
    var spec = new CategoryByIdSpec(id);
    var category = await _categoryRepository.FirstOrDefaultAsync(spec);
    if (category == null)
    {
      return NotFound();
    }
    var categoryVO = new CategoryValueObject
    {
      Id = category.Id,
      Name = category.Name,
      Description = category.Description
    };
    return View(categoryVO);
  }

  // POST: Categories/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost("edit/{id:int}")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Id")] CategoryValueObject categoryValueObject)
  {
    if (id != categoryValueObject.Id)
    {
      return NotFound();
    }

    if (ModelState.IsValid)
    {
      var spec = new CategoryByIdSpec(id);
      var category = await _categoryRepository.FirstOrDefaultAsync(spec);
      if (category == null) { return NotFound(); }
      category.updateCategory(categoryValueObject);
      await _categoryRepository.UpdateAsync(category);
      await _categoryRepository.SaveChangesAsync();

      return RedirectToAction(nameof(Index));
    }
    return View(categoryValueObject);
  }

  // GET: Categories/Delete/5
  [HttpGet("delete/{id:int}")]
  public async Task<IActionResult> Delete(int id)
  {
    var spec = new CategoryByIdSpec(id);
    var category = await _categoryRepository.FirstOrDefaultAsync(spec);
    if (category == null)
    {
      return NotFound();
    }
    await _categoryRepository.DeleteAsync(category);

    var categoryVO = new CategoryValueObject
    {
      Id = category.Id,
      Name = category.Name,
      Description = category.Description,
    };
    return View(categoryVO);
  }
  // POST: Categories/Delete/5
  [HttpPost("delete/{id:int}")]
  //[HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {

    var spec = new CategoryByIdSpec(id);

    var category = await _categoryRepository.FirstOrDefaultAsync(spec);
    if (category != null)
    {
      await _categoryRepository.DeleteAsync(category);
    }

    await _categoryRepository.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }

}
