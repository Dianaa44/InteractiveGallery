
using Ardalis.Specification;


namespace InteractiveGallery.Core.ArtistAggregate.Specifications;
public class ArtistByIdSpec : Specification<Artist>, ISingleResultSpecification
{
  public ArtistByIdSpec(int artistId)
  {
    Query
        .Where(artist => artist.Id == artistId);
  }
}
