
using Ardalis.Specification;


namespace InteractiveGallery.Core.ArtistAggregate.Specifications;
public class ArtistsAtGalleryIdSpec : Specification<GalleryArtist>, ISingleResultSpecification
{
  public ArtistsAtGalleryIdSpec(int galleryId)
  {
    Query
        .Where(galleryArtist => galleryArtist.GalleryId == galleryId);
  }
}


