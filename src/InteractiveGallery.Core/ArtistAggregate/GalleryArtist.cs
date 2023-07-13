using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveGallery.Core.GalleryAggregate;
using InteractiveGallery.SharedKernel;

namespace InteractiveGallery.Core.ArtistAggregate;
public class GalleryArtist : EntityBase
{

  public int ArtistId { get; set; }
  public int GalleryId { get; set; }
  public virtual Artist Artist { get; set; }                  
  public virtual Gallery Gallery { get; set; }      
  public GalleryArtist()
  {
   
  }

}
