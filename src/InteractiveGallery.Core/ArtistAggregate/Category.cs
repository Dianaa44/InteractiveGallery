using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using InteractiveGallery.SharedKernel;

namespace InteractiveGallery.Core.ArtistAggregate;
public class Category : EntityBase
{
  
  public string Name { get; private set; }
  public string Description { get; set; }
  public virtual List<Artwork> Artworks { get; private set; }

  public Category(string name)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Artworks = new List<Artwork>();
  }
}
