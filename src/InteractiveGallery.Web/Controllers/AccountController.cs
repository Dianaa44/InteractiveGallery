using InteractiveGallery.Core.ArtistAggregate;
using InteractiveGallery.Core.ArtistAggregate.Specifications;
using InteractiveGallery.Core.GalleryAggregate;
using InteractiveGallery.Infrastructure.Data;
using InteractiveGallery.SharedKernel.Interfaces;
using InteractiveGallery.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InteractiveGallery.Web.Controllers;
//[Route("[controller]")]
public class AccountController : Controller
{
  private readonly IRepository<Artist> _artistRepository;
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly IRepository<Gallery> _galleryRepository;


  public AccountController(IRepository<Artist> artistRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IRepository<Gallery> galleryRepository)
  {
    _artistRepository = artistRepository;
    _userManager = userManager;
    _signInManager = signInManager;
    _galleryRepository= galleryRepository;
  }


  public IActionResult Index()
  {
    return View();
  }

  //[HttpGet("register")]
  [HttpGet]
  public IActionResult Register()
  {
    var viewModel = new RegistrationViewModel();
    return View(viewModel);
  }

  //[HttpPost("register")]
  [HttpPost]
  public async Task<IActionResult> Register(RegistrationViewModel viewModel)
  {
    if (ModelState.IsValid)
    {
      // Create Identity User using viewModel.Email and viewModel.Password
      var user = new ApplicationUser { UserName = viewModel.Email, Email = viewModel.Email };
      if (user == null) { return View("Error"); }
      var result = await _userManager.CreateAsync(user, viewModel.Password);
      await _userManager.AddToRoleAsync((ApplicationUser)user, "User");

      if (result.Succeeded)
      {
        // Create Artist record using viewModel.ArtistName and viewModel.ArtistBio
        var artist = new Artist(viewModel.Name,viewModel.Biography);
        artist.IdentityGuid = user.Id; // Assign the user ID to the artist
        await _artistRepository.AddAsync(artist);
        await _artistRepository.SaveChangesAsync();

        // Sign in the user (optional)
        await _signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToAction("Create", "Galleries", new { area = "UserArea" }); // Redirect to a success page
      }
      else
      {
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError("", error.Description);
        }
      }
    }

    return View(viewModel);
  }
}
