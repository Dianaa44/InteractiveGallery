using Ardalis.Specification;

namespace InteractiveGallery.Core.GalleryAggregate.Specifications;
public class GalleryByInitiatorArtistIdSpec : Specification<Gallery>, ISingleResultSpecification
{
  public GalleryByInitiatorArtistIdSpec(int InitiatorArtistId)
  {
    Query
        .Where(gallery => gallery.InitiatorId == InitiatorArtistId);
  }
}
