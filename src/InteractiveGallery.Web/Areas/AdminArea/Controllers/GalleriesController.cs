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
using Microsoft.AspNetCore.Authorization;
using System.Data;
using InteractiveGallery.Core.CategoryAggregate;
using InteractiveGallery.Core.CategoryAggregate.Specifications;

namespace InteractiveGallery.Web.Areas.AdminArea.Controllers;
[Area("AdminArea")]
[Route("AdminArea/[controller]")]
[Authorize(Roles = "Admin")]
public class GalleriesController : Controller
    {
  private readonly IRepository<Gallery> _galleryRepository;
  private readonly IRepository<Artist> _artistRepository;
  private readonly IRepository<Category> _categoryRepository;
  private readonly UserManager<ApplicationUser> _userManager;


  public GalleriesController(IRepository<Gallery> galleryRepository, IRepository<Artist> artistRepository, IRepository<Category> categoryRepository,UserManager<ApplicationUser> userManager)
  {
    _galleryRepository = galleryRepository;
    _artistRepository = artistRepository;
    _userManager = userManager;
    _categoryRepository = categoryRepository;
  }


  // GET: Galleries
  [HttpGet]
  public async Task<IActionResult> Index()
        {
    var galleries = await _galleryRepository.ListAsync();
  
    return View(galleries.ToArray());
  }

  [HttpGet("details/{id:int}")]
  public async Task<IActionResult> Details(int id)
  {
    var spec = new GalleryByIdSpec(id);
    var gallery = await _galleryRepository.FirstOrDefaultAsync(spec);
    if (gallery == null)
    {
      return NotFound();
    }
    var artistSpec = new ArtistByIdSpec(gallery.InitiatorId);
    var artist = await _artistRepository.FirstOrDefaultAsync(artistSpec);
    var galleryVO = new GalleryValueObject
    {
      Id = gallery.Id,
      Name = gallery.Name,
      Theme = gallery.Theme,
      InitiatorId = gallery.InitiatorId,
      InitiatorArtist = artist,
      Artworks = gallery.Artworks
    };
    return View(galleryVO);
  }



  // GET: Galleries/Details/5
  [HttpGet("detailsGallery")]
  public  IActionResult DetailsGallery()
  {
    return View();
  }

  // GET: Galleries/Create
  [HttpGet("create")]
  public async Task<IActionResult> CreateAsync()
        {
    var artists = await _artistRepository.ListAsync();
    List<SelectListItem> selectListGalleries = artists
    .Select(c => new SelectListItem
    {
      Value = c.Id.ToString(),
      Text = c.Name
    })
    .ToList();
    ViewBag.artists = selectListGalleries;
    return View();
        }

  // POST: Galleries/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost("create")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(GalleryValueObject galleryValueObject)
        {
    var artistSpec = new ArtistByIdSpec(galleryValueObject.InitiatorId);
    var artist = await _artistRepository.FirstOrDefaultAsync(artistSpec);
    galleryValueObject.InitiatorArtist = artist;
    var gallery = new Gallery(galleryValueObject);
    
    if (ModelState.IsValid)
    {
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
    var artistSpec= new ArtistByIdSpec(gallery.InitiatorId);
    var artist = await _artistRepository.FirstOrDefaultAsync(artistSpec);
    var galleryVO = new GalleryValueObject
    {
      Id = gallery.Id,
      Name = gallery.Name,
      Theme = gallery.Theme,
      InitiatorId = gallery.InitiatorId,
      InitiatorArtist = artist
    };
    return View(galleryVO);
  }

  // POST: Galleries/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost("edit/{id:int}")]
  [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Theme,InitiatorArtist,InitiatorId,Id")] GalleryValueObject galleryValueObject)
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
    var artistSpec = new ArtistByIdSpec(gallery.InitiatorId);
    var artist = await _artistRepository.FirstOrDefaultAsync(artistSpec);
    var galleryVO = new GalleryValueObject
    {

      Id = gallery.Id,
      Name = gallery.Name,
      Theme = gallery.Theme,
      InitiatorId = gallery.InitiatorId,
      InitiatorArtist = artist
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
//  [HttpGet("{galleryId:int}/editArtworkModal/{artworkId:int}")]
  //[HttpGet]
  public async Task<IActionResult> EditArtworkModal(int galleryId, int artworkId)
  {
    var spec = new GalleryByIdSpec(galleryId);
    var gallery = await _galleryRepository.FirstOrDefaultAsync(spec);
    if(gallery==null)return NotFound();
    var artwork = gallery.getArtworkById(artworkId);
    var artworkVO = new ArtworkValueObject(artwork);

    return PartialView("EditArtworkModal",
        artworkVO
        );
  }
 // [HttpPost("/{galleryId:int}/editArtworkModal/{artworkId:int}")]
  //[HttpPost]
  public async Task<IActionResult> EditArtworkModal(int galleryId,int artworkId, [Bind("Name,Price,Status,CategoryId,ArtistId,GalleryId,Image,Description,Id")] ArtworkValueObject artworkVO)
  {
    if (artworkVO == null) return NotFound();
    if (artworkId != artworkVO.Id)
    {
      return NotFound();
    }
    if (ModelState.IsValid)
    {
    var spec = new GalleryByIdSpec(galleryId);
    var gallery = await _galleryRepository.FirstOrDefaultAsync(spec);
    if (gallery == null) return NotFound();
      gallery.updateArtwork(artworkVO);
      return RedirectToAction(nameof(Index));
    }
    return View(artworkVO);
  }

  [HttpGet("details/{galleryId:int}/editArtwork/{artworkId:int}")]
  public async Task<IActionResult> EditArtwork(int galleryId,int artworkId)
  {
    var spec = new GalleryByIdSpec(galleryId);
    var gallery = await _galleryRepository.FirstOrDefaultAsync(spec);
    if (gallery == null) return NotFound();
    var artwork = gallery.getArtworkById(artworkId);
    var artworkVO = new ArtworkValueObject(artwork);
    if (artwork.ArtistId == null) return NotFound();
    int artistid = (int)artwork.ArtistId;
    var artistspec = new ArtistByIdSpec(artistid);
    var artist = await _artistRepository.FirstOrDefaultAsync(artistspec);
    if (artist == null) return NotFound();


    List<SelectListItem> selectListGalleries = artist.Galleries
    .Select(c => new SelectListItem
    {
      Value = c.Id.ToString(),
      Text = c.Name
    })
    .ToList();
    ViewBag.galleries = selectListGalleries;

    ViewBag.artist = artist.Id;
    var categories = await _categoryRepository.ListAsync();
    List<SelectListItem> selectListCategories = categories
    .Select(c => new SelectListItem
    {
      Value = c.Id.ToString(),
      Text = c.Name
    })
    .ToList();
    ViewBag.categories = selectListCategories;

    return View(artworkVO);
  }

  // POST: Artworks/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost("details/{galleryId:int}/editArtwork/{artworkId:int}")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> EditArtwork(int galleryId,int artworkId, [Bind("Name,Price,Status,CategoryId,ArtistId,GalleryId,Image,Description,Id")] ArtworkValueObject artworkVO)
  { if (artworkId != artworkVO.Id) return NotFound();
    if (ModelState.IsValid) { 
    var spec = new GalleryByIdSpec(galleryId);
    var gallery = await _galleryRepository.FirstOrDefaultAsync(spec);
    if (gallery == null) return NotFound();
    var artistspec = new ArtistByIdSpec(artworkVO.ArtistId);
    var artist = await _artistRepository.FirstOrDefaultAsync(artistspec);
    if (artist == null) return NotFound();
    var catgoryspec = new CategoryByIdSpec(artworkVO.CategoryId);
    var category = await _categoryRepository.FirstOrDefaultAsync(catgoryspec);
    if (category == null) return NotFound();
    var artwork = new Artwork(artworkVO,artist,gallery,category);
    artist.updateArtwork(artwork);
    await _artistRepository.SaveChangesAsync();
    await _galleryRepository.UpdateAsync(gallery);
    await _galleryRepository.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(artworkVO);
  }

  //  GET: Artworks/Delete/5
  [HttpGet("details/{galleryId:int}/deleteArtwork/{artworkId:int}")]
  public async Task<IActionResult> DeleteArtwork(int galleryId,int artworkId)
  {

    var spec = new GalleryByIdSpec(galleryId);
    var gallery = await _galleryRepository.FirstOrDefaultAsync(spec);
    if (gallery == null) return NotFound();
    var artwork = gallery.getArtworkById(artworkId);
    if (artwork == null) return NotFound();
    return View(artwork);
  }

  // POST: Artworks/Delete/5
  [HttpPost("details/{galleryId:int}/deleteArtwork/{artworkId:int}")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteArtworkConfirmed(int galleryId, int artworkId)
  {
    var spec = new GalleryByIdSpec(galleryId);
    var gallery = await _galleryRepository.FirstOrDefaultAsync(spec);
    if (gallery == null) return NotFound();
    var artwork = gallery.getArtworkById(artworkId);
    if (artwork == null) return NotFound();
    if (artwork.ArtistId == null) return NotFound();
    int artistid = (int)artwork.ArtistId;
    var artistspec = new ArtistByIdSpec(artistid);
    var artist = await _artistRepository.FirstOrDefaultAsync(artistspec);
    if (artist == null) return NotFound();
    artist.deleteArtwork(artwork);
    gallery.deleteArtwork(artwork);
    artwork.deleteRefrences();
    await _galleryRepository.UpdateAsync(gallery);
    await _galleryRepository.SaveChangesAsync();
    await _artistRepository.UpdateAsync(artist);
    await _artistRepository.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }
  


}

