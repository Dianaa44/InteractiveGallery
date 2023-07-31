using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace InteractiveGallery.Core.ArtistAggregate.Specifications;
public class ArtworksByArtistIdSpec: Specification<Artwork>
{
  public ArtworksByArtistIdSpec(int artistId)
  {
    Query
        .Where(artwork => artwork.ArtistId==artistId);
  }
}

