namespace InteractiveGallery.Web.ViewModels;


public class MultiStepRegisterViewModel
{
  // Step 1 - User registration (IdentityUser)
  public string Email { get; set; } = "";
  public string Password { get; set; } = "";

  // Step 2 - Artist information
  public string Name { get; set; } = "";
  public string Bio { get; set; } = "";

}
