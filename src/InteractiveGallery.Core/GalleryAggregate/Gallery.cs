using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using InteractiveGallery.Core.ArtistAggregate;
using InteractiveGallery.SharedKernel;
using InteractiveGallery.SharedKernel.Interfaces;

namespace InteractiveGallery.Core.GalleryAggregate;
public class Gallery : EntityBase, IAggregateRoot
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
  public Gallery(GalleryValueObject galleryValueObject) {
  Name = galleryValueObject.Name;
  Theme = galleryValueObject.Theme;
  InitiatorId = galleryValueObject.InitiatorId;

  }

  public void updateGallery(GalleryValueObject galleryValueObject)
  {
    Name = galleryValueObject.Name;
    Theme = galleryValueObject.Theme;
    InitiatorId = galleryValueObject.InitiatorId;
    InitiatorArtist = galleryValueObject.InitiatorArtist;
  }
}
