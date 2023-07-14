using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InteractiveGallery.Core.ArtistAggregate;
using InteractiveGallery.Infrastructure.Data;
using InteractiveGallery.SharedKernel.Interfaces;
using InteractiveGallery.Core.ArtistAggregate.Specifications;

namespace InteractiveGallery.Web.Controllers;
[Route("[controller]")]
public class ArtistsController : Controller
    {
  private readonly IRepository<Artist> _artistRepository;

  public ArtistsController(IRepository<Artist> artistRepository, AppDbContext context)
  {
    _artistRepository = artistRepository;
    _context = context;
  }

  private readonly AppDbContext _context;
  //      public ArtistsController(AppDbContext context)
  //      {
  //          _context = context;
  //      }

  // GET: Artists
  [HttpGet]
  public async Task<IActionResult> Index()
  {
    var artists = await _artistRepository.ListAsync();
    return View(artists.ToArray());
  }

  // GET: Artists/Details/5
  //  [HttpGet]
  //[HttpGet("{id:int}")]
  //[Route("details")]     
  [HttpGet("details/{id:int}")]
  public async Task<IActionResult> Details(int id)
  {
    var spec = new ArtistByIdSpec(id);
    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
    if (artist == null)
    {
      return NotFound();
    }

    var artistVO = new ArtistValueObject
    {
      Id = artist.Id,
      Name = artist.Name,
      Biography = artist.Biography
    };
    return View(artistVO);
  }

  // GET: Artists/Create
  [HttpGet("create")]
  public IActionResult Create()
        {
            return View();
        }

  
  [HttpPost("create")]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(ArtistValueObject artistValueObject)
  {
    var artist = new Artist(artistValueObject);
    if (ModelState.IsValid)
    {   
      await _artistRepository.AddAsync(artist);
      await _artistRepository.SaveChangesAsync();   ///unit of work?
      return RedirectToAction(nameof(Index));
    }
    return View(artist);
  }

  // GET: Artists/Edit/5
  [HttpGet("edit/{id:int}")]
  public async Task<IActionResult> Edit(int id)
  {
      var spec = new ArtistByIdSpec(id);
      var artist = await _artistRepository.FirstOrDefaultAsync(spec);
              if (artist == null)
              {
                  return NotFound();
              }
      var artistVO = new ArtistValueObject
      {
        Id = artist.Id,
        Name = artist.Name,
        Biography = artist.Biography
      };
      return View(artistVO);
          }

  // POST: Artists/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost("edit/{id:int}")]
  [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Biography,Id")] ArtistValueObject artistValueObject)
        {
            if (id != artistValueObject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                 var spec = new ArtistByIdSpec(id);
                 var artist= await _artistRepository.FirstOrDefaultAsync(spec);
      if(artist == null) { return NotFound(); }
                 artist.updateArtist(artistValueObject);
                 await _artistRepository.UpdateAsync(artist);
                 await _artistRepository.SaveChangesAsync();
               
                return RedirectToAction(nameof(Index));
            }
            return View(artistValueObject);
        }

  // GET: Artists/Delete/5
  [HttpGet("delete/{id:int}")]
  public async Task<IActionResult> Delete(int id)
        {

    var spec = new ArtistByIdSpec(id);
    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
    if (artist == null)
    {
      return NotFound();
    }
    await _artistRepository.DeleteAsync(artist);

    var artistVO = new ArtistValueObject
    {
      Id = artist.Id,
      Name = artist.Name,
      Biography = artist.Biography
    };
    return View(artistVO);
  }

  // POST: Artists/Delete/5
  [HttpPost("delete/{id:int}")]
  //[HttpPost, ActionName("Delete")]
 [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
    //if (_context.Artists == null)
    //{
    //    return Problem("Entity set 'AppDbContext.Artists'  is null.");
    //}
    var spec = new ArtistByIdSpec(id);

    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
            if (artist != null)
            {
               await _artistRepository.DeleteAsync(artist);
            }
            
            await _artistRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }

