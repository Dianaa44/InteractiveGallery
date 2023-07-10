using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveGallery.Core.ArtistAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InteractiveGallery.Infrastructure.Data.Config;
public class GalleryArtistConfiguration : IEntityTypeConfiguration<GalleryArtist>
{
  public void Configure(EntityTypeBuilder<GalleryArtist> builder)
  {
    builder.HasIndex(e => e.ArtistId);

    builder.HasIndex(e => e.GalleryId);

    builder.Property(e => e.Id)
                        .HasColumnName("id")
                        .ValueGeneratedOnAdd();

    builder.HasOne(a => a.Artist)
                       .WithMany(b => b.JoinedGalleries)
                       .HasForeignKey(d => d.ArtistId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_GalleryArtist_Artist");
    builder.HasOne(a => a.Gallery)
                       .WithMany(b => b.Artists)
                       .HasForeignKey(d => d.GalleryId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_GalleryArtist_Gallery");

  }
}
