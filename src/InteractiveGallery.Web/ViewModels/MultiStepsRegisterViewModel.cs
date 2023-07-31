namespace InteractiveGallery.Web.ViewModels;


public class MultiStepsegisterViewModel
{
  // Step 1 - User registration (IdentityUser)
  public string Email { get; set; } = "";
  public string Password { get; set; } = "";
  public string ConfirmPassword { get; set; } = "";
   public string PhoneNumber { get; set; } = "";
  // Step 2 - Artist information
  public string Name { get; set; } = "";
  public string Biography { get; set; } = "";
  public IFormFile? ProfilePicture { get; set; }

}
