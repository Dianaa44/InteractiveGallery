using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InteractiveGallery.Core.GalleryAggregate;
using InteractiveGallery.Infrastructure.Data;
using InteractiveGallery.Core.ArtistAggregate;
using InteractiveGallery.SharedKernel.Interfaces;
using InteractiveGallery.Core.ArtistAggregate.Specifications;
using InteractiveGallery.Core.GalleryAggregate.Specifications;
using Microsoft.AspNetCore.Identity;

namespace InteractiveGallery.Web.Controllers;
[Route("[controller]")]
public class GalleriesController : Controller
    {
  private readonly IRepository<Gallery> _galleryRepository;
  private readonly IRepository<Artist> _artistRepository;
  private readonly UserManager<ApplicationUser> _userManager;


  public GalleriesController(IRepository<Gallery> galleryRepository, IRepository<Artist> artistRepository, UserManager<ApplicationUser> userManager)
  {
    _galleryRepository = galleryRepository;
    _artistRepository = artistRepository;
    _userManager = userManager;
  }


  // GET: Galleries
  [HttpGet]
  public async Task<IActionResult> Index()
        {
    var galleries = await _galleryRepository.ListAsync();
    return View(galleries.ToArray());
  }

  // GET: Galleries/Details/5
  [HttpGet("details/{id:int}")]
  public async Task<IActionResult> Details(int id)
  {
    var spec = new GalleryByIdSpec(id);
    var gallery = await _galleryRepository.FirstOrDefaultAsync(spec);
    if (gallery == null)
    {
      return NotFound();
    }

    var galleryVO = new GalleryValueObject
    {
      Id = gallery.Id,
      Name = gallery.Name,
      Theme = gallery.Theme,
    };
    return View(galleryVO);
  }

  // GET: Galleries/Create
  [HttpGet("create")]
  public IActionResult Create()
        {
            return View();
        }

  // POST: Galleries/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost("create")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(GalleryValueObject galleryValueObject)
        {
    
    string? artistIdentityGuid= _userManager.GetUserId(HttpContext.User);
    var initiatorspec = new ArtistByIdentityGuidSpec(artistIdentityGuid);
    var initiatorUser = await _artistRepository.FirstOrDefaultAsync(initiatorspec);
    if (initiatorUser == null) { return NotFound(); } 
    //galleryValueObject.InitiatorArtist = initiatorUser;
    var gallery = new Gallery(galleryValueObject);
    if (ModelState.IsValid)
    {
      initiatorUser.addGallery(gallery);
      await _artistRepository.UpdateAsync(initiatorUser);
      await _artistRepository.SaveChangesAsync();
      await _galleryRepository.AddAsync(gallery);
      await _galleryRepository.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
            return View(gallery);
        }

  // GET: Galleries/Edit/5
  [HttpGet("edit/{id:int}")]
  public async Task<IActionResult> Edit(int id)
        {
    var spec = new GalleryByIdSpec(id);
    var gallery = await _galleryRepository.FirstOrDefaultAsync(spec);
    if (gallery == null)
    {
      return NotFound();
    }
    var galleryVO = new GalleryValueObject
    {

      Id = gallery.Id,
      Name = gallery.Name,
      Theme = gallery.Theme,
    };
    return View(galleryVO);
  }

  // POST: Galleries/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost("edit/{id:int}")]
  [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Theme,InitiatorId,Id")] GalleryValueObject galleryValueObject)
        {
    if (id != galleryValueObject.Id)
    {
      return NotFound();
    }

    if (ModelState.IsValid)
    {
      var spec = new GalleryByIdSpec(id);
      var gallery = await _galleryRepository.FirstOrDefaultAsync(spec);
      if (gallery == null) { return NotFound(); }
      gallery.updateGallery(galleryValueObject);
      await _galleryRepository.UpdateAsync(gallery);
      await _galleryRepository.SaveChangesAsync();

      return RedirectToAction(nameof(Index));
    }
    return View(galleryValueObject);
  }


  // GET: Galleries/Delete/5
  [HttpGet("delete/{id:int}")]
  public async Task<IActionResult> Delete(int id)
        {

    var spec = new GalleryByIdSpec(id);
    var gallery = await _galleryRepository.FirstOrDefaultAsync(spec);
    if (gallery == null)
    {
      return NotFound();
    }
    await _galleryRepository.DeleteAsync(gallery);

    var galleryVO = new GalleryValueObject
    {

      Id = gallery.Id,
      Name = gallery.Name,
      Theme = gallery.Theme,
    };
    return View(galleryVO);
  }

  // POST: Galleries/Delete/5
  [HttpPost("delete/{id:int}")]
  [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

    var spec = new GalleryByIdSpec(id);

    var gallery = await _galleryRepository.FirstOrDefaultAsync(spec);
    if (gallery != null)
    {
      await _galleryRepository.DeleteAsync(gallery);
    }

    await _galleryRepository.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }

}

