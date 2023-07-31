using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using InteractiveGallery.Core.ArtistAggregate;

namespace InteractiveGallery.Core.GalleryAggregate.Specifications;
public class GalleryByIdSpec : Specification<Gallery>, ISingleResultSpecification
{
  public GalleryByIdSpec(int galleryId)
  {
    Query
        .Where(gallery => gallery.Id == galleryId).Include(gallery=>gallery.Artworks);
  }
}

