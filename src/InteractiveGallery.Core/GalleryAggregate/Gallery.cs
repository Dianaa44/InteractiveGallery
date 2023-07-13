using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using InteractiveGallery.Core.ArtistAggregate;
using InteractiveGallery.SharedKernel;

namespace InteractiveGallery.Core.GalleryAggregate;
public class Gallery : AggregateRoot
{
  public string Name { get; private set; }
  public string Theme { get; private set; }
  public int InitiatorId { get; private set; }
  public virtual ICollection<GalleryArtist> Artists { get; private set; }
  public virtual ICollection<Artwork> Artworks { get; private set; }
  public virtual Artist InitiatorArtist { get; private set; }

  public Gallery()
  {
  
  }
}
