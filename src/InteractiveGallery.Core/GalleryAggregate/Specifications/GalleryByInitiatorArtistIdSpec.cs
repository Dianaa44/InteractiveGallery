using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
