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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using InteractiveGallery.Core.GalleryAggregate;
using InteractiveGallery.Core.GalleryAggregate.Specifications;
using InteractiveGallery.Web.ViewModels;
using InteractiveGallery.Core.CategoryAggregate;
using InteractiveGallery.Core.CategoryAggregate.Specifications;

namespace InteractiveGallery.Web.Areas.UserArea.Controllers;

[Area("UserArea")]
[Route("UserArea/[controller]")]
[Authorize(Roles = "User")]
public class ArtistsController : Controller
    {
  private readonly IRepository<Artist> _artistRepository;
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IRepository<Gallery> _galleryRepository;
  private readonly IRepository<Category> _categoryRepository;
  private readonly IWebHostEnvironment _webHostEnvironment;


  public ArtistsController(IRepository<Artist> artistRepository, UserManager<ApplicationUser> userManager, IRepository<Gallery> galleryRepository, IRepository<Category> categoryRepository, IWebHostEnvironment webHostEnvironment)
  {
    _artistRepository = artistRepository;
    _userManager = userManager;
    _galleryRepository = galleryRepository;
    _categoryRepository = categoryRepository;
    _webHostEnvironment = webHostEnvironment;
  }
   
  [HttpGet]
  public async Task<IActionResult> Index()
  {
    var artists = await _artistRepository.ListAsync();
    return View(artists.ToArray());
  }

  // GET: Artists/Details/5     
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
      Biography = artist.Biography,
    };
    var gallerySpec = new GalleryByInitiatorArtistIdSpec(artist.Id);
    artistVO.Galleries = await _galleryRepository.ListAsync(gallerySpec);
    ViewBag.galleries = artistVO.Galleries;
    return View(artistVO);
  }
  [HttpGet("MyArtworks")]
  public async Task<IActionResult> MyArtworks()
  {
    string? identityGuid = _userManager.GetUserId(HttpContext.User);
    var spec = new ArtistByIdentityGuidSpec(identityGuid);
    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
    if (artist == null)
    {
      return NotFound();
    }
    return View(artist);
  }
 


  // GET: Artists/Edit/5
  [HttpGet("editprofile")]
  public async Task<IActionResult> Edit()
  {
    string? identityGuid = _userManager.GetUserId(HttpContext.User);
    var spec = new ArtistByIdentityGuidSpec(identityGuid);
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
  [HttpPost("editprofile")]
  [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( [Bind("Name,Biography,Id")] ArtistValueObject artistValueObject)
        {
    string? identityGuid = _userManager.GetUserId(HttpContext.User);
    var userSpec = new ArtistByIdentityGuidSpec(identityGuid);
    var artist = await _artistRepository.FirstOrDefaultAsync(userSpec);
    if (artist == null)
    {
      return NotFound();
    }
    if (artist.Id != artistValueObject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
    
    var spec = new ArtistByIdSpec(id);

    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
            if (artist != null)
            {
               await _artistRepository.DeleteAsync(artist);
            }
            
            await _artistRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


  
  //***************************************** Artwork Actions *************************************************
  // GET: Artworks/Create
  [HttpGet("createArtwork")]
  public async Task<IActionResult> CreateArtworkAsync()
  {
    string? identityGuid = _userManager.GetUserId(HttpContext.User);
    var spec = new ArtistByIdentityGuidSpec(identityGuid);
    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
    if (artist == null)
    {
      return NotFound();
    }
    var gallerySpec = new GalleryByInitiatorArtistIdSpec(artist.Id);
    var galleries = await _galleryRepository.ListAsync(gallerySpec);
    List<SelectListItem> selectListGalleries = galleries
    .Select(c => new SelectListItem
    {
      Value = c.Id.ToString(),
      Text = c.Name
    })
    .ToList();
    ViewBag.galleries = selectListGalleries;

    ViewBag.artist = artist;
    var categories = await _categoryRepository.ListAsync();
    List<SelectListItem> selectListCategories = categories
    .Select(c => new SelectListItem
    {
      Value = c.Id.ToString(),
      Text = c.Name
    })
    .ToList();
    ViewBag.categories = selectListCategories;

    return View();
  }




  // POST: Artworks/Create
  //[HttpPost]
  [HttpPost("createArtwork")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> CreateArtworkAsync( ArtworkValueObject artworkValueObject)
  {
    string? identityGuid = _userManager.GetUserId(HttpContext.User);
    var spec = new ArtistByIdentityGuidSpec(identityGuid);
    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
    if (artist == null)
    {
      return NotFound();
    }
    artworkValueObject.Status = ArtworkStatus.Available;
    artworkValueObject.ArtistId = artist.Id;
    //artworkValueObject.Artist = artist;

    string filename = UploadFile(artworkValueObject);
    artworkValueObject.Image = filename;

    var specCategory = new CategoryByIdSpec(artworkValueObject.CategoryId);
    var specGallery = new GalleryByIdSpec(artworkValueObject.GalleryId);
    //artworkValueObject.Category = await _categoryRepository.FirstOrDefaultAsync(specCategory);
    var gallery = await _galleryRepository.FirstOrDefaultAsync(specGallery);
    if (gallery == null) { return NotFound(); }
    var artwork = new Artwork(artworkValueObject);

    if (ModelState.IsValid)
    {
      artist.addArtwork(artwork);
      //var gallery = artworkValueObject.Gallery;
     // await _galleryRepository.UpdateAsync(gallery);
     // await _galleryRepository.SaveChangesAsync();
      await _artistRepository.UpdateAsync(artist);
      await _artistRepository.SaveChangesAsync();
      return RedirectToAction(nameof(MyArtworks));
    }
    return View(artist);
  }

  private string UploadFile(ArtworkValueObject artworkVO)
  {
    string filename = "";
    if (artworkVO.theImage != null)
    {
      string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "ArtworkImages");
      filename = Guid.NewGuid().ToString() + "_" + artworkVO.theImage.FileName;
      string filepath = Path.Combine(uploadDir, filename);
      using (var fileStream = new FileStream(filepath, FileMode.Create))
      {
        artworkVO.theImage.CopyTo(fileStream);
      }
    }
    return filename;

  }


  ///Artwork
  [HttpGet("myArtworks/editArtwork/{artworkId:int}")]
  public async Task<IActionResult> EditArtwork (int artworkId) {
    ///make sure the user is the artwork artist

    string? identityGuid = _userManager.GetUserId(HttpContext.User);
    var spec = new ArtistByIdentityGuidSpec(identityGuid);
    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
    if (artist == null)
    {
      return NotFound();
    }

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
    var artwork = artist.getArtworkById(artworkId);
    if(artwork == null) { return NotFound(); }
    var artworkVO = new ArtworkValueObject(artwork);
    return View(artworkVO);
  }

  // POST: Artworks/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost("myArtworks/editArtwork/{artworkId:int}")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> EditArtwork( int artworkId, [Bind("Name,Price,Status,CategoryId,ArtistId,GalleryId,Image,Description,Id")] ArtworkValueObject artworkVO)
  {
    if (artworkId != artworkVO.Id) return NotFound();
    if (ModelState.IsValid)
    {
      string? identityGuid = _userManager.GetUserId(HttpContext.User);
      var spec = new ArtistByIdentityGuidSpec(identityGuid);
      var artist = await _artistRepository.FirstOrDefaultAsync(spec);
      if (artist == null)
      {
        return NotFound();
      }
      var gallerySpec = new GalleryByIdSpec(artworkVO.GalleryId);
      var gallery = await _galleryRepository.FirstOrDefaultAsync(gallerySpec);
      if (gallery == null) { return NotFound(); }
      var catgoryspec = new CategoryByIdSpec(artworkVO.CategoryId);
      var category = await _categoryRepository.FirstOrDefaultAsync(catgoryspec);
      if (category == null) return NotFound();
      var artwork = new Artwork(artworkVO, artist, gallery, category);
      artist.updateArtwork(artwork);
      await _artistRepository.UpdateAsync(artist);
      await _artistRepository.SaveChangesAsync();      
      //await _galleryRepository.UpdateAsync(gallery);
      //await _galleryRepository.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(artworkVO);
  }

  //  GET: Artworks/Delete/5
  [HttpGet("myArtworks/deleteArtwork/{artworkId:int}")]
  public async Task<IActionResult> DeleteArtwork( int artworkId)
  {
    string? identityGuid = _userManager.GetUserId(HttpContext.User);
    var spec = new ArtistByIdentityGuidSpec(identityGuid);
    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
    if (artist == null)
    {
      return NotFound();
    }
    var artwork = artist.getArtworkById(artworkId);
    if (artwork == null) return NotFound();
    return View(artwork);
  }

  // POST: Artworks/Delete/5
  [HttpPost("myArtworks/deleteArtwork/{artworkId:int}")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteArtworkConfirmed( int artworkId)
  {
    string? identityGuid = _userManager.GetUserId(HttpContext.User);
    var spec = new ArtistByIdentityGuidSpec(identityGuid);
    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
    if (artist == null)
    {
      return NotFound();
    }
    var artwork = artist.getArtworkById(artworkId);
    if (artwork == null) return NotFound();

    if (artwork.GalleryId != null)
    {

      var gallerySpec = new GalleryByIdSpec((int)artwork.GalleryId);
      var gallery = await _galleryRepository.FirstOrDefaultAsync(gallerySpec);
      if (gallery == null) { return NotFound(); }
      gallery.deleteArtwork(artwork);
      await _galleryRepository.UpdateAsync(gallery);
      await _galleryRepository.SaveChangesAsync();
    }
    artist.deleteArtwork(artwork);
    await _artistRepository.UpdateAsync(artist);
    await _artistRepository.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }
}


