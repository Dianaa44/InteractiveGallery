using System.Reflection;
using InteractiveGallery.Core.ArtistAggregate;
using InteractiveGallery.Core.GalleryAggregate;
using InteractiveGallery.SharedKernel;
using InteractiveGallery.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InteractiveGallery.Infrastructure.Data;
public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }
  private readonly IDomainEventDispatcher? _dispatcher;

  public AppDbContext(DbContextOptions options,
    IDomainEventDispatcher? dispatcher)
      : base(options)
  {
    _dispatcher = dispatcher;
  }
  /*public DbSet<Project> Projects => Set<Project>();
  public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
  public DbSet<Contributor> Contributors => Set<Contributor>();
  */
  //Modification
  public DbSet<Artist> Artists => Set<Artist>();
  public DbSet<Artwork> Artworks => Set<Artwork>();
  public DbSet<Gallery> Galleries => Set<Gallery>();
  public DbSet<Category> Categories => Set<Category>();
  public DbSet<GalleryArtist> GalleriesArtist => Set<GalleryArtist>();



  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_dispatcher == null) return result;

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
        .Select(e => e.Entity)
        .Where(e => e.DomainEvents.Any())
        .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }
}
