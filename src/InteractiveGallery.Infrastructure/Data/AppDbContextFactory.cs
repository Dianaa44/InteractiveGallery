
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace InteractiveGallery.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<InteractiveGalleryDbContext>
{
  public InteractiveGalleryDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<InteractiveGalleryDbContext>();
    optionsBuilder.UseSqlServer("Server=DESKTOP-L4R1NQ5\\SQL2022;Database=interactiveGallery; User Id = sa;" +
                                 " Password=Diana2022;Trusted_Connection=True;TrustServerCertificate=True;");

    return new InteractiveGalleryDbContext(optionsBuilder.Options);
  }
}
