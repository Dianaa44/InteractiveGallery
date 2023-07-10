using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using InteractiveGallery.Core.ArtistAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InteractiveGallery.Infrastructure.Data.Config;
public class ArtworkConfiguration : IEntityTypeConfiguration<Artwork>
{
  public void Configure(EntityTypeBuilder<Artwork> builder)
  {

    builder.Property(e => e.Id)
                        .HasColumnName("id")
                        .ValueGeneratedOnAdd();


    builder.Property(a => a.Name)
                          .HasMaxLength(100).IsRequired()
                          .HasColumnName("name");


    builder.Property(a => a.Description)
                              .HasColumnName("description");


    builder.Property(a => a.Price)
                            .HasColumnName("price");


    builder.Property(a => a.Image)
                          .IsRequired()
                          .HasColumnType("text")
                          .HasColumnName("image");

    builder.HasOne(a => a.Artist)
                   .WithMany(b => b.Artworks)
                   .HasForeignKey(d => d.ArtistId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Artwork_Artist");

    builder.HasOne(a => a.Category)
                   .WithMany(b => b.Artworks)
                   .HasForeignKey(d => d.CategoryId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Artwork_Category");

    builder.HasOne(a => a.Gallery)
               .WithMany(b => b.Artworks)
               .HasForeignKey(d => d.GalleryId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Artwork_Gallery");
    builder.Property(a => a.Status)
                .HasConversion<string>()
                .IsRequired().HasColumnName("status");

  }
}
