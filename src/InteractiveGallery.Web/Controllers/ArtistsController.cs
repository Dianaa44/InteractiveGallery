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
  public IActionResult Index()
  {

    return View();
  }

  // GET: Artists/Details/5
  public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Artists == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

  // GET: Artists/Create
  [HttpGet("create")]
  public IActionResult Create()
        {
            return View();
        }

  // POST: Artists/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  //[HttpPost]
  //[ValidateAntiForgeryToken]
  //public async Task<IActionResult> Create([Bind("Name,Biography,Id")] Artist artist)
  //{
  //    if (ModelState.IsValid)
  //    {
  //        var artists = await _artistRepository.AddAsync(artist);  
  //        //_context.Add(artist);
  //        await _artistRepository.SaveChangesAsync();
  //        return RedirectToAction(nameof(Index));
  //    }
  //    return View(artist);
  //}
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
  public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Artists == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Biography,Id")] Artist artist)
        {
            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Artists == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Artists == null)
            {
                return Problem("Entity set 'AppDbContext.Artists'  is null.");
            }
            var artist = await _context.Artists.FindAsync(id);
            if (artist != null)
            {
                _context.Artists.Remove(artist);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
          return (_context.Artists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

