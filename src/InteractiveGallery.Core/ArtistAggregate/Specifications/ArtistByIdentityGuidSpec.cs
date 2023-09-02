using Ardalis.Specification;

namespace InteractiveGallery.Core.ArtistAggregate.Specifications;
public class ArtistByIdentityGuidSpec : Specification<Artist>, ISingleResultSpecification
{
  public ArtistByIdentityGuidSpec(string identityGuid)
  {
    Query
        .Where(artist => artist.IdentityGuid == identityGuid).
        Include(artist=>artist.Artworks).Include(artist=>artist.Galleries);
  }
}
