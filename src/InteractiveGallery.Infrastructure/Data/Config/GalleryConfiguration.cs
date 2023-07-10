
using InteractiveGallery.Core.GalleryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InteractiveGallery.Infrastructure.Data.Config;
public class GalleryConfiguration : IEntityTypeConfiguration<Gallery>
{
    public void Configure(EntityTypeBuilder<Gallery> builder)

{
  builder.Property(e => e.Id)
                        .HasColumnName("id")
                        .ValueGeneratedOnAdd();


  builder.Property(a => a.Name)
                          .HasMaxLength(100).IsRequired()
                          .HasColumnName("name");


  builder.Property(a => a.Theme)
                              .HasColumnName("theme");




  builder.HasOne(a => a.InitiatorArtist)
                   .WithMany(b=>b.Galleries)
                   .HasForeignKey(d => d.InitiatorId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Gallery_Artist"); //

}
}
