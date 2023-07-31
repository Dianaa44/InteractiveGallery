using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveGallery.Core.ArtistAggregate;

namespace InteractiveGallery.Core.GalleryAggregate;
public class GalleryValueObject
{
  public int Id { get; set; }
  public string Name { get;  set; }
  public string Theme { get;  set; }
  public int InitiatorId { get;  set; }
  //public virtual ICollection<GalleryArtist> Artists { get; private set; }
  public  ICollection<Artwork> Artworks { get;  set; }
  public  Artist InitiatorArtist { get;  set; }
}
