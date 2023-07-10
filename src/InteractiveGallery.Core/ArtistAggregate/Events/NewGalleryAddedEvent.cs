
using InteractiveGallery.Core.GalleryAggregate;
using InteractiveGallery.SharedKernel;

namespace InteractiveGallery.Core.ArtistAggregate.Events;
internal class NewGalleryAddedEvent : DomainEventBase
{

  public Gallery gallery { get; set; }
  public Artist artist { get; set; }

  public NewGalleryAddedEvent(Artist artist,
      Gallery gallery)
  {
    this.artist = artist;
    this.gallery=gallery;
  }
}
