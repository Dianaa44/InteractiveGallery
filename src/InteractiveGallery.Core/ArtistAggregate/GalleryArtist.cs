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
  public GalleryArtistStatus Status { get; set; }
  public virtual Artist Artist { get; set; }                  
  public virtual Gallery Gallery { get; set; }      
  public GalleryArtist()
  {
   
  }
  public GalleryArtist(Gallery gallery,Artist artist)
  {
    this.Artist = artist;
    this.ArtistId= artist.Id;
    this.Gallery = gallery;
    this.GalleryId = gallery.Id;
    this.Status = GalleryArtistStatus.Pending;
  }

}
