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
using InteractiveGallery.Core.CategoryAggregate.Specifications;
using Microsoft.AspNetCore.Hosting;
using InteractiveGallery.Core.CategoryAggregate;

namespace InteractiveGallery.Web.Areas.AdminArea.Controllers;

[Area("AdminArea")]
[Route("AdminArea/[controller]")]
[Authorize(Roles = "Admin")]
public class ArtistsController : Controller
    {
  private readonly IRepository<Artist> _artistRepository;
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IRepository<Gallery> _galleryRepository;
  private readonly IRepository<Category> _categoryRepository;
  private readonly IWebHostEnvironment _webHostEnvironment;


  public ArtistsController(IRepository<Artist> artistRepository,UserManager<ApplicationUser> userManager,IRepository<Gallery> galleryRepository, IRepository<Category> categoryRepository, IWebHostEnvironment webHostEnvironment)
  {
    _artistRepository = artistRepository;
    _userManager = userManager;
    _galleryRepository = galleryRepository;
    _categoryRepository = categoryRepository;
    _webHostEnvironment = webHostEnvironment;
  }

  [HttpGet ]
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

  // GET: Artists/Create
  [HttpGet("create")]
  public IActionResult Create()
        {
            return View();
        }

  
  [HttpPost("create")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(ArtistValueObject artistValueObject)
  {
    var artist = new Artist(artistValueObject);
    if (ModelState.IsValid)
    { 
      artist.IdentityGuid=_userManager.GetUserId(HttpContext.User);
      await _artistRepository.AddAsync(artist);
      await _artistRepository.SaveChangesAsync();   
      return RedirectToAction(nameof(Index));
    }
    return View(artist);
  }


  // GET: Artists/Edit/5
  [HttpGet("edit/{id:int}")]
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
  [HttpPost("edit/{id:int}")]
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


  [HttpGet("register")]
  public IActionResult Register()
  {
    return View();
  }

  [HttpPost("register")]
  public IActionResult Register(MultiStepsegisterViewModel model)
  {
    if (ModelState.IsValid)
    {
      // Process the artist registration data and create a new artist account
      // Redirect to a success page or login page after successful registration.
      return RedirectToAction(nameof(Index));
    }

    // If the model state is invalid, redisplay the artist register view with validation errors.
    return View(model);
  }









  ///////////////////////////////////////////////Artwork/////////////////////////////////////////////////////////

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

  // GET: Artworks/Create
  [HttpGet("createArtwork/{artistId:int}")]
  public async Task<IActionResult> CreateArtworkAsync(int artistId)
  {
    var spec = new ArtistByIdSpec(artistId);
    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
    if (artist == null)
    {
      return NotFound();
    }
    var gallerySpec = new GalleryByInitiatorArtistIdSpec(artistId);
    var galleries = await _galleryRepository.ListAsync(gallerySpec);
    List<SelectListItem> selectListGalleries = galleries
    .Select(c => new SelectListItem
    {
      Value = c.Id.ToString(),
      Text = c.Name
    })
    .ToList();
    ViewBag.galleries = selectListGalleries;

    //ViewBag.joinedGalleries = artist.JoinedGalleries.ToList();
    ViewBag.artist = artist;
    //Is category a diffrent aggregate?
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
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  //[HttpPost]
  [HttpPost("createArtwork/{artistId:int}")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> CreateArtworkAsync(int artistId, ArtworkValueObject artworkValueObject)
  {
    
    var initiatorspec = new ArtistByIdSpec(artistId);
    var artist = await _artistRepository.FirstOrDefaultAsync(initiatorspec);
    if (artist == null) { return NotFound(); }
    artworkValueObject.Status = ArtworkStatus.Available;
    artworkValueObject.ArtistId = artist.Id;
    artworkValueObject.Artist = artist;
    
    string filename = UploadFile(artworkValueObject);
    artworkValueObject.Image = filename;

    var specCategory = new CategoryByIdSpec(artworkValueObject.CategoryId);
    var specGallery = new GalleryByIdSpec(artworkValueObject.GalleryId);
    artworkValueObject.Category = await _categoryRepository.FirstOrDefaultAsync(specCategory);
    artworkValueObject.Gallery = await _galleryRepository.FirstOrDefaultAsync(specGallery);

    var artwork = new Artwork(artworkValueObject);

    if (ModelState.IsValid)
    {
      artist.addArtwork(artwork);
      var gallery = artworkValueObject.Gallery;
      if (gallery == null) { return NotFound(); }
      await _galleryRepository.UpdateAsync(gallery);
      await _artistRepository.SaveChangesAsync();
      await _artistRepository.UpdateAsync(artist);
      await _artistRepository.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(artist);
  }


  [HttpGet("{artistId:int}/editArtwork/{artworkId:int}")]

  public async Task<IActionResult> Edit(int artistId,int artworkId)
  {
    var spec = new ArtistByIdSpec(artistId);
    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
    if (artist == null)
    {
      return NotFound();
    }

    var gallerySpec = new GalleryByInitiatorArtistIdSpec(artistId);
    var galleries = await _galleryRepository.ListAsync(gallerySpec);
    List<SelectListItem> selectListGalleries = galleries
    .Select(c => new SelectListItem
    {
      Value = c.Id.ToString(),
      Text = c.Name
    })
    .ToList();
    ViewBag.galleries = selectListGalleries;

    //ViewBag.joinedGalleries = artist.JoinedGalleries.ToList();
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


    var artwork = artist.getArtworkbyId(artworkId);
    if (artwork == null)
    {
      return NotFound();
    }
    var artworkVO = new ArtworkValueObject
    {
      Id = artwork.Id,
      Name = artwork.Name,
      Price = artwork.Price,
      Category = artwork.Category,
      CategoryId = artwork.CategoryId,
      Artist = artwork.Artist,
      ArtistId = artwork.ArtistId,
      Description = artwork.Description,
      Gallery = artwork.Gallery,
      GalleryId = artwork.GalleryId,
      Image = artwork.Image,
      Status = artwork.Status,
    };


    return View(artwork);
  }

  // POST: Artworks/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost("{artistId:int}/editArtwork/{artworkId:int}")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int artworkId, [Bind("Name,Price,Status,CategoryId,ArtistId,GalleryId,Image,Description,Id")] ArtworkValueObject artworkValueObject)
  {
    if (artworkId != artworkValueObject.Id)
    {
      return NotFound();
    }

    if (ModelState.IsValid)
    {
      int artistId = 1;
      var spec = new ArtistByIdSpec(artistId);
      var artist = await _artistRepository.FirstOrDefaultAsync(spec);
      if (artist == null)
      {
        return NotFound();
      }
      artist.updateArtwork(artworkValueObject);
      await _artistRepository.UpdateAsync(artist);
      await _artistRepository.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(artworkValueObject);
  }
}


