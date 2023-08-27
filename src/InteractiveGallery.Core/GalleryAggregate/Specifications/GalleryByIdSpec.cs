using Ardalis.Specification;

namespace InteractiveGallery.Core.GalleryAggregate.Specifications;
public class GalleryByIdSpec : Specification<Gallery>, ISingleResultSpecification
{
  public GalleryByIdSpec(int galleryId)
  {
    Query
        .Where(gallery => gallery.Id == galleryId).Include(gallery => gallery.Artworks) ;
  }
}

