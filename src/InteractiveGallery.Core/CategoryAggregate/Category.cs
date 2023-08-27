using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using InteractiveGallery.Core.ArtistAggregate;
using InteractiveGallery.SharedKernel;
using InteractiveGallery.SharedKernel.Interfaces;

namespace InteractiveGallery.Core.CategoryAggregate;
public class Category : EntityBase,IAggregateRoot
{
  public string Name { get; private set; }
  public string Description { get; set; }
  public virtual List<Artwork> Artworks { get; private set; }

  //public Category(string name)
  //{
  //  Name = Guard.Against.NullOrEmpty(name, nameof(name));
  //  Artworks = new List<Artwork>();
  //}
  public Category(CategoryValueObject categoryValueObject)
  {
    Name=categoryValueObject.Name;
    Description=categoryValueObject.Description;
  }
  public Category(string name, string description)
  {
    Name = name;
    Description = description;
  }

  public void updateCategory(CategoryValueObject categoryValueObject)
  {
    this.Name = categoryValueObject.Name;
    this.Description = categoryValueObject.Description;
  }
  public void updateCategory(string name, string description)
  {
    Name = name;
    Description = description;
  }

}
