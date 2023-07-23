﻿using Ardalis.GuardClauses;
using InteractiveGallery.SharedKernel;
using InteractiveGallery.SharedKernel.Interfaces;
using InteractiveGallery.Core.ArtistAggregate.Events;
using InteractiveGallery.Core.GalleryAggregate;

namespace InteractiveGallery.Core.ArtistAggregate;
public class Artist : EntityBase , IAggregateRoot
{
  public string IdentityGuid { get;  set; }
  public string Name { get;  private set; }
  public string Biography { get; private set; }
  public virtual List<Gallery> Galleries { get; private set; }
  public virtual List<Artwork> Artworks { get; private set; }
  public virtual List<GalleryArtist> JoinedGalleries { get; private set; }

  public Artist(string identity) : this()
  {
    Guard.Against.NullOrEmpty(identity, nameof(identity));
    IdentityGuid = identity;
  }



  public Artist(string name,string biography)
  {
    //Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Name = name;
    Biography = biography;
    Artworks = new List<Artwork>();
    Galleries = new List<Gallery>();
    JoinedGalleries = new List<GalleryArtist>();

  }
  public Artist() { }

  public Artist(ArtistValueObject artistValueObject)
  {
    Name=artistValueObject.Name;
    Biography=artistValueObject.Biography;
    Artworks = new List<Artwork>();
    Galleries = new List<Gallery>();
    JoinedGalleries = new List<GalleryArtist>();


  }
  //public void AddGallery(Gallery gallery)
  //{
  //  Guard.Against.Null(gallery, nameof(gallery));
  //  Galleries.Add(gallery);
  //  RaiseDomainEvent(new NewGalleryAddedEvent(this, gallery));
  //  var newGalleryAddedEvent = new NewGalleryAddedEvent(this, gallery);
  //  base.RegisterDomainEvent(newGalleryAddedEvent);
  //}

  public void updateName(string name)
  {
    if (string.IsNullOrEmpty(name)) Name = name;
  }
  public void updateArtist(ArtistValueObject artistValueObject)
  {
    Name = artistValueObject.Name;
    Biography = artistValueObject.Biography;
  }

  public void addArtwork(Artwork artwork)
  {
    if (Artworks ==  null) Artworks=new List<Artwork>();
    Artworks.Add(artwork);
  }

  public Artwork getArtworkbyId(int id)
  {
    foreach (Artwork artwork in Artworks)
    {
      if (artwork.Id == id)
        return artwork;
    }  
    return null;
  }

  public void addGallery(Gallery gallery) {
    if (Galleries == null) Galleries = new List<Gallery>();
    Galleries.Add(gallery);

    }

  public void updateArtwork(ArtworkValueObject artworkValueObject)
  {

  }

 
}

