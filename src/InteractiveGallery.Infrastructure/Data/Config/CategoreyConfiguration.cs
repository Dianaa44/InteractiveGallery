using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveGallery.Core.ArtistAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InteractiveGallery.Infrastructure.Data.Config;
public class CategoreyConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
{

    builder.Property(e => e.Id)
                          .HasColumnName("id")
                          .ValueGeneratedOnAdd();


    builder.Property(a => a.Name)
                            .HasMaxLength(100).IsRequired()
                            .HasColumnName("name");


    builder.Property(a => a.Description)
                                .HasColumnName("description");

  }

}
