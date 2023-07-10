using Ardalis.Specification.EntityFrameworkCore;
using InteractiveGallery.SharedKernel;
using InteractiveGallery.SharedKernel.Interfaces;

namespace InteractiveGallery.Infrastructure.Data;
// inherit from Ardalis.Specification type
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T: AggregateRoot
{
  public EfRepository(AppDbContext dbContext) : base(dbContext)
  {

  }
}
