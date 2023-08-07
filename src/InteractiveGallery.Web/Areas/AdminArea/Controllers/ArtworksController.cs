//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using InteractiveGallery.Core.ArtistAggregate;
//using InteractiveGallery.Infrastructure.Data;
//using InteractiveGallery.SharedKernel.Interfaces;
//using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
//using InteractiveGallery.Core.ArtistAggregate.Specifications;
//using SQLitePCL;
//using InteractiveGallery.Core.GalleryAggregate;
//using InteractiveGallery.Core.GalleryAggregate.Specifications;
//using InteractiveGallery.Core.CategoryAggregate;
//using InteractiveGallery.Core.CategoryAggregate.Specifications;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;

//namespace InteractiveGallery.Web.Areas.AdminArea.Controllers;
//[Route("[controller]")]
//public class ArtworksController : Controller
//    {
//  private readonly IRepository<Artist> _artistRepository;
//  private readonly IRepository<Gallery> _galleryRepository;
//  private readonly IRepository<Category> _categoryRepository;
//  private readonly IWebHostEnvironment _webHostEnvironment;
//  private readonly UserManager<ApplicationUser> _userManager;


//  public ArtworksController(IRepository<Artist> artistRepository, IRepository<Gallery> galleryRepository, IRepository<Category> categoryRepository, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
//  {
//    _artistRepository = artistRepository;
//    _galleryRepository=galleryRepository;
//    _categoryRepository=categoryRepository;
//    _webHostEnvironment=webHostEnvironment;
//    _userManager=userManager;
//  }

//  // GET: Artworks

//  [HttpGet]
//  public async Task<IActionResult> Index()
//        {
//    //artist????????
//    int artistId=1;
//    var spec = new ArtistByIdSpec(artistId);
//    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
//    if (artist == null)
//    {
//      return NotFound();
//    }
//    //??????????????????????????????artworks is null !!!!
//    var artworks = artist.Artworks;
//    //artist.getArtworks();
//    return View(artworks);

//  }

//  // GET: Artworks/Details/5
//  [HttpGet("details/{artworkId}")]
//  public async Task<IActionResult> Details(int artworkId)
//        {
//    //artist Id
//    int artistId = 1;
//    var spec = new ArtistByIdSpec(artistId);
//    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
//    if (artist == null)
//    {
//      return NotFound();
//    }
//    var artwork = artist.getArtworkbyId(artworkId);
//    var artworkVO = new ArtworkValueObject(artwork);
//    return View(artworkVO);
//  }

//  // GET: Artworks/Create
//  [HttpGet("create")]
//  public async Task<IActionResult> CreateAsync()
//        {
//    int artistId = 1;
//    var spec = new ArtistByIdSpec(artistId);
//    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
//    if (artist == null)
//    {
//      return NotFound();
//    }
//    var gallerySpec = new GalleryByInitiatorArtistIdSpec(artistId);
//    var galleries = await _galleryRepository.ListAsync(gallerySpec);
//    List<SelectListItem> selectListGalleries = galleries
//    .Select(c => new SelectListItem
//    {
//      Value = c.Id.ToString(),
//      Text = c.Name
//    })
//    .ToList();
//    ViewBag.galleries = selectListGalleries;

//    //ViewBag.joinedGalleries = artist.JoinedGalleries.ToList();
//    ViewBag.artist = artist;
//    //Is category a diffrent aggregate?
//    var categories= await _categoryRepository.ListAsync();
//    List<SelectListItem> selectListCategories = categories
//    .Select(c => new SelectListItem
//    {
//      Value = c.Id.ToString(),
//      Text = c.Name
//    })
//    .ToList();
//    ViewBag.categories = selectListCategories;
   
//            return View();
//        }

//        // POST: Artworks/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        //[HttpPost]
//  [HttpPost("create")]
//  [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(ArtworkValueObject artworkValueObject)
//        {
//    string? artistIdentityGuid = _userManager.GetUserId(HttpContext.User);
//    var initiatorspec = new ArtistByIdentityGuidSpec(artistIdentityGuid);
//    var artistUser = await _artistRepository.FirstOrDefaultAsync(initiatorspec);
//    if (artistUser == null) { return NotFound(); }
//    artworkValueObject.Status = ArtworkStatus.Available;
//    artworkValueObject.ArtistId = artistUser.Id;
//    artworkValueObject.Artist = artistUser;
//    var spec = new ArtistByIdSpec(artistUser.Id);
//    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
//    if (artist == null)
//    { return NotFound(); }
    
//    string filename = UploadFile(artworkValueObject);
//    artworkValueObject.Image = filename;

//    var specCategory= new CategoryByIdSpec(artworkValueObject.CategoryId);
//    var specGallery = new GalleryByIdSpec(artworkValueObject.GalleryId);
//    artworkValueObject.Category =await _categoryRepository.FirstOrDefaultAsync(specCategory);
//    artworkValueObject.Gallery = await _galleryRepository.FirstOrDefaultAsync(specGallery);
    
//    var artwork = new Artwork(artworkValueObject);
  
//    if (ModelState.IsValid)
//    {
//      artist.addArtwork(artwork);
//      var gallery = artworkValueObject.Gallery;
//      if (gallery == null) { return NotFound(); }
//      await _galleryRepository.UpdateAsync(gallery);
//      await _artistRepository.SaveChangesAsync();
//      await _artistRepository.UpdateAsync(artist);
//      await _artistRepository.SaveChangesAsync();
//      return RedirectToAction(nameof(Index));
//    }
//    return View(artist);
//  }

//  // GET: Artworks/Edit/5
//  [HttpGet("edit/{artworkId}")]

//  public async Task<IActionResult> Edit(int artworkId)
//        {
//    int artistId = 1;
//    var spec = new ArtistByIdSpec(artistId);
//    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
//    if (artist == null)
//    { 
//      return NotFound();
//    }
 
//    var gallerySpec = new GalleryByInitiatorArtistIdSpec(artistId);
//    var galleries = await _galleryRepository.ListAsync(gallerySpec);
//    List<SelectListItem> selectListGalleries = galleries
//    .Select(c => new SelectListItem
//    {
//      Value = c.Id.ToString(),
//      Text = c.Name
//    })
//    .ToList();
//    ViewBag.galleries = selectListGalleries;

//    //ViewBag.joinedGalleries = artist.JoinedGalleries.ToList();
//    ViewBag.artist = artist;
//    //Is category a diffrent aggregate?
//    var categories = await _categoryRepository.ListAsync();
//    List<SelectListItem> selectListCategories = categories
//    .Select(c => new SelectListItem
//    {
//      Value = c.Id.ToString(),
//      Text = c.Name
//    })
//    .ToList();
//    ViewBag.categories = selectListCategories;


//    var artwork = artist.getArtworkbyId(artworkId);
//    if (artwork==null)
//            {
//                return NotFound();
//            }
//    var artworkVO = new ArtworkValueObject(artwork);

//            return View(artwork);
//        }

//        // POST: Artworks/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int artworkId, [Bind("Name,Price,Status,CategoryId,ArtistId,GalleryId,Image,Description,Id")] ArtworkValueObject artworkValueObject)
//        {
//            if (artworkId != artworkValueObject.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//      int artistId = 1;
//      var spec = new ArtistByIdSpec(artistId);
//      var artist = await _artistRepository.FirstOrDefaultAsync(spec);
//      if (artist == null)
//      {
//        return NotFound();
//      }
//      artist.updateArtwork(artworkValueObject);      
//      await _artistRepository.UpdateAsync(artist);
//      await _artistRepository.SaveChangesAsync();
//      return RedirectToAction(nameof(Index));
//            }
//            return View(artworkValueObject);
//        }
//  /*
//        // GET: Artworks/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.Artworks == null)
//            {
//                return NotFound();
//            }

//            var artwork = await _context.Artworks
//                .Include(a => a.Artist)
//                .Include(a => a.Category)
//                .Include(a => a.Gallery)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (artwork == null)
//            {
//                return NotFound();
//            }

//            return View(artwork);
//        }

//        // POST: Artworks/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.Artworks == null)
//            {
//                return Problem("Entity set 'InteractiveGalleryDbContext.Artworks'  is null.");
//            }
//            var artwork = await _context.Artworks.FindAsync(id);
//            if (artwork != null)
//            {
//                _context.Artworks.Remove(artwork);
//            }
            
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool ArtworkExists(int id)
//        {
//          return (_context.Artworks?.Any(e => e.Id == id)).GetValueOrDefault();
//        }*/


//  private string UploadFile(ArtworkValueObject artworkVO)
//  {
//    string filename = "";
//    if (artworkVO.theImage != null)
//    {
//      string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "ArtworkImages");
//      filename = Guid.NewGuid().ToString() + "_" + artworkVO.theImage.FileName;
//      string filepath = Path.Combine(uploadDir, filename);
//      using (var fileStream = new FileStream(filepath, FileMode.Create))
//      {
//        artworkVO.theImage.CopyTo(fileStream);
//      }
//    }
//    return filename;

//  }



//  private void deleteImage(ArtworkValueObject artworkVO)
//  {
//    if (!string.IsNullOrEmpty(artworkVO.Image))
//    {
//      var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", artworkVO.Image.TrimStart('/'));
//      if (System.IO.File.Exists(imagePath))
//      {    System.IO.File.Delete(imagePath);
//      }
//    }

//  }
//}

