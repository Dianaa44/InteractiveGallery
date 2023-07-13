using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace InteractiveGallery.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
  public AppDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=interactiveGallery;Trusted_Connection=True;MultipleActiveResultSets=true");

    return new AppDbContext(optionsBuilder.Options);
  }
}
