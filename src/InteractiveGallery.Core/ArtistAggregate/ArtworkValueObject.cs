using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveGallery.Core.CategoryAggregate;
using InteractiveGallery.Core.GalleryAggregate;
using Microsoft.AspNetCore.Http;

namespace InteractiveGallery.Core.ArtistAggregate;
public class ArtworkValueObject
{
  public int Id { get; set; }
  public string Name { get;  set; }
  public double Price { get;  set; }
  public ArtworkStatus Status { get; set; }
  public int CategoryId { get; set; }
  public int ArtistId { get;  set; }
  public int GalleryId { get; set; }
  public string Image { get;  set; }
  public IFormFile theImage { get;  set; }
  public string Description { get;  set; }
  public virtual Category Category { get;  set; }
  public virtual Gallery Gallery { get;  set; }
  public virtual Artist Artist { get; set; }
  


}
