using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace InteractiveGallery.Core.ArtistAggregate.Specifications;
public class ArtistByIdentityGuidSpec : Specification<Artist>, ISingleResultSpecification
{
  public ArtistByIdentityGuidSpec(string identityGuid)
  {
    Query
        .Where(artist => artist.IdentityGuid == identityGuid);
  }
}
