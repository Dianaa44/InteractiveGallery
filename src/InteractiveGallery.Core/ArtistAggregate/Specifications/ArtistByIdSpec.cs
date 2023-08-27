using Ardalis.Specification;

namespace InteractiveGallery.Core.ArtistAggregate.Specifications;
public class ArtistByIdSpec : Specification<Artist>, ISingleResultSpecification
{
  public ArtistByIdSpec(int artistId)
  {
    Query
        .Where(artist => artist.Id == artistId).Include(artist=>artist.Artworks).Include(artist=>artist.Galleries);
  }
}
