using InteractiveGallery.Core.ArtistAggregate;
using InteractiveGallery.Core.ArtistAggregate.Specifications;
using InteractiveGallery.SharedKernel.Interfaces;
using InteractiveGallery.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InteractiveGallery.Web.Controllers;

[Route("[controller]")]
public class ArtistController: Controller
{
  private readonly IRepository<Artist> _artistRepository;

  public ArtistController(IRepository<Artist> artistRepository)
  {
    _artistRepository = artistRepository;
  }




  public IActionResult Index()
  {
    return View();
  }

  /*
     // GET artist/{artistId?}
  [HttpGet("{artistId:int}")]public async Task<IActionResult> Index(int artistId = 1)
  {
    var spec = new ArtistByIdSpec(artistId);
    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
    if (artist == null)
    {
      return NotFound();
    }

    var theArtist = new ArtistViewModel
    {
      Id = artist.Id,
      Name = artist.Name,
      Biography = artist.Biography,
      Galleries=artist.Galleries,
      Artworks=artist.Artworks,
    };
    return View(theArtist);
  }*/


  /*public async Task<IActionResult> Index()
  {
    var spec = new ArtistByIdSpec(1);///add new spec (select all)
    var artists = await _artistRepository.ListAsync(spec);
    var artistViewModels = artists.Select(a => new ArtistViewModel
    {
      Id = a.Id,
      Name = a.Name,
      Biography = a.Biography    });
    return View(artistViewModels);
  }
  */
  public IActionResult Create()
  {
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(ArtistViewModel artistViewModel)
  {
    if (ModelState.IsValid)
    {
      var artist = new Artist(1, artistViewModel.Name);   //زبطي شغلة ال ID
    await _artistRepository.AddAsync(artist);
      await _artistRepository.SaveChangesAsync();   ///unit of work?
      return RedirectToAction(nameof(Index));
    }
    return View(artistViewModel);
  }

  public async Task<IActionResult> Edit(int id)
  {
    var spec = new ArtistByIdSpec(id);
    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
    if (artist == null)
    {
      return NotFound();
    }
    var artistViewModel = new ArtistViewModel
    {
      Id = artist.Id,
      Name = artist.Name,
      Biography = artist.Biography
    };
    return View(artistViewModel);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, ArtistViewModel artistViewModel)
  {
    if (id != artistViewModel.Id)
    {
      return NotFound();
    }
    if (ModelState.IsValid)
    {
      var spec = new ArtistByIdSpec(id);
      var artist = await _artistRepository.FirstOrDefaultAsync(spec);
      if (artist == null)
      {
        return NotFound();
      }
      artist.updateName(artistViewModel.Name);
      artist.Biography = artistViewModel.Biography;
      await _artistRepository.UpdateAsync(artist);
      await _artistRepository.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(artistViewModel);
  }

  public async Task<IActionResult> Delete(int id)
  {

    var spec = new ArtistByIdSpec(id);
    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
    if (artist == null)
    {
      return NotFound();
    }
    var artistViewModel = new ArtistViewModel
    {
      Id = artist.Id,
      Name = artist.Name,
      Biography = artist.Biography
    };
    return View(artistViewModel);
  }

  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {

    var spec = new ArtistByIdSpec(id);
    var artist = await _artistRepository.FirstOrDefaultAsync(spec);
    if (artist == null)
    {
      return NotFound();
    }
    await _artistRepository.DeleteAsync(artist);
    await _artistRepository.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }
}



