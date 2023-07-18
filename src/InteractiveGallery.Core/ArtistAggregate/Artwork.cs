using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using InteractiveGallery.SharedKernel;
using Ardalis.GuardClauses;
using InteractiveGallery.Core.GalleryAggregate;

namespace InteractiveGallery.Core.ArtistAggregate;
public class Artwork : EntityBase
{
 public String Name { get; private set; }
 public double Price { get; private set; } 

 public string  Status { get; private set; }
  public int CategoryId { get; private set; }
 public int ArtistId { get; private set; }
  public int GalleryId { get; private set; }
  public string Image { get; private set; }
 public string Description { get; private set; }

 //[NotMapped]
 //public IFormFile theImage { get; private set; }
 public virtual Category Category { get; private set; }
  public virtual Gallery Gallery { get; private set; }
  public virtual Artist Artist { get; private set; }


  //////
  //we should get the string path from the IFormFile here
  /////

  public Artwork( string name, string image,string status)

  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Status = Status;
    Image = image;
    // ArtistId = artistId;

    //GalleryId = galleryId;

    //  CategoryId = categoryId;
  }


}
