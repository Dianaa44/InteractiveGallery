using InteractiveGallery.Core.ArtistAggregate;
using InteractiveGallery.Core.GalleryAggregate;

namespace InteractiveGallery.Web.ViewModels;

public class ArtistViewModel
{
  public int Id { get; set; }

  public required string Name { get; set; } 

  public string? Biography { get; set; }

  public  List<Gallery> Galleries = new();
  public  List<Artwork> Artworks = new();
}
