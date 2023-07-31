using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using InteractiveGallery.Core.ArtistAggregate;

namespace InteractiveGallery.Core.GalleryAggregate.Specifications;
public class ArtworksByGalleryIdSpec: Specification<Artwork>
{
  public ArtworksByGalleryIdSpec(int galleryId)
  {
    Query
        .Where(artwork => artwork.GalleryId== galleryId);
  }
}

