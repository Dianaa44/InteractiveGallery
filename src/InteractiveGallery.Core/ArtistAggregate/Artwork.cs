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
using InteractiveGallery.Core.CategoryAggregate;

namespace InteractiveGallery.Core.ArtistAggregate;
public class Artwork : EntityBase
{
 public String Name { get; private set; }
 public double Price { get; private set; } 

  public ArtworkStatus  Status { get; private set; }
  public int CategoryId { get; private set; }
 public int? ArtistId { get; private set; }
  public int? GalleryId { get; private set; }
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

  public Artwork( string name, string image)

  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Image = image;
    // ArtistId = artistId;

    //GalleryId = galleryId;

    //  CategoryId = categoryId;
  }
  public Artwork(ArtworkValueObject artworkValueObject)
  {
    this.Status = artworkValueObject.Status;
    this.Name=artworkValueObject.Name;
    this.Description=artworkValueObject.Description;
    this.Image = artworkValueObject.Image;
    this.Price = artworkValueObject.Price;
    this.CategoryId = artworkValueObject.CategoryId;
    this.ArtistId = artworkValueObject.ArtistId;
    this.GalleryId= artworkValueObject.GalleryId;

    //this.Artist= artworkValueObject.Artist;
    //this.Gallery= artworkValueObject.Gallery;
    //this.Category= artworkValueObject.Category;
  }


  public Artwork(ArtworkValueObject artworkValueObject,Artist artist,Gallery gallery,Category category)
  {
    this.Id=artworkValueObject.Id;
    this.Status = artworkValueObject.Status;
    this.Name = artworkValueObject.Name;
    this.Description = artworkValueObject.Description;
    this.Image = artworkValueObject.Image;
    this.Price = artworkValueObject.Price;
    this.CategoryId = artworkValueObject.CategoryId;
    this.ArtistId = artworkValueObject.ArtistId;
    this.GalleryId = artworkValueObject.GalleryId;

    this.Artist= artist;
    this.Gallery= gallery;
    this.Category= category;
  }
  public void update(Artwork artwork)
  {
    this.Status = artwork.Status;
    this.Name = artwork.Name;
    this.Description = artwork.Description;
    this.Image = artwork.Image;
    this.Price = artwork.Price;
    this.CategoryId = artwork.CategoryId;
    this.ArtistId = artwork.ArtistId;
    this.GalleryId = artwork.GalleryId;
    this.Category = artwork.Category;
    this.Artist = artwork.Artist;
    this.Gallery= artwork.Gallery;
  }
  public void deleteRefrences()
  {
    this.ArtistId = null;
    this.Artist = null;
    this.GalleryId = null;
    this.Gallery = null;
  }

  public void deleteGalleryRefrences()
  {
    this.GalleryId = null;
    this.Gallery = null; 
  }
}
