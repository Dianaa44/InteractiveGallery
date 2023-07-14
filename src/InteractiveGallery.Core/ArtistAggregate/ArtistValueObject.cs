using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveGallery.Core.GalleryAggregate;

namespace InteractiveGallery.Core.ArtistAggregate;
public class ArtistValueObject
{
  public int Id { get;  set; }
  public string Name { get; set; }
  public string Biography { get; set; }
  public virtual List<Gallery> Galleries { get;  set; }
  public virtual List<Artwork> Artworks { get;  set; }
  public virtual List<GalleryArtist> JoinedGalleries { get;  set; }
  public ArtistValueObject(string name, string biography)
  {
    //Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Name = name;
    Biography = biography;
    Artworks = new List<Artwork>();
    Galleries = new List<Gallery>();
    JoinedGalleries = new List<GalleryArtist>();

  }
  public ArtistValueObject() { }

}
