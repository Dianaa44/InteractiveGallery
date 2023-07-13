using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.AspNetCore.Http.Internal;

namespace InteractiveGallery.Core.ArtistAggregate.Specifications;
public class AllArtistsSpec : Specification<Artist>
{ 
  public AllArtistsSpec()
  {
    Query
        .Where(artist => artist.Id.GetType().Equals("int"));
  }

}
