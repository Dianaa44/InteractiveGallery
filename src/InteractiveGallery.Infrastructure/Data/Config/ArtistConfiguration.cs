using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveGallery.Core.ArtistAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InteractiveGallery.Infrastructure.Data.Config;
public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
  public void Configure(EntityTypeBuilder<Artist> builder)
  {

    builder.Property(e => e.Id)
                        .HasColumnName("id")
                        .ValueGeneratedOnAdd(); 

    builder.Property(a => a.Name)
        .HasMaxLength(100)
        .IsRequired().HasColumnName("name");

    builder.Property(a => a.Biography).HasColumnName("biography");
  }
}
