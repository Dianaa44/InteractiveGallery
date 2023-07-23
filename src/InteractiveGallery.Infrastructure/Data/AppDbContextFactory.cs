using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace InteractiveGallery.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<InteractiveGalleryDbContext>
{
  public InteractiveGalleryDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<InteractiveGalleryDbContext>();
    optionsBuilder.UseSqlServer("Server=DESKTOP-9KU8EQC;Database=interactiveGallery; User Id = sa; Password=Diana2022;Trusted_Connection=True;TrustServerCertificate=True;");
   // optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=interactiveGallery;Trusted_Connection=True;MultipleActiveResultSets=true");

    return new InteractiveGalleryDbContext(optionsBuilder.Options);
  }
}
