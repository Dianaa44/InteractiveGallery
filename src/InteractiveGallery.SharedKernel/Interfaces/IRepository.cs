using Ardalis.Specification;

namespace InteractiveGallery.SharedKernel.Interfaces;
// from Ardalis.Specification
public interface IRepository<T> : IRepositoryBase<T> where T : AggregateRoot
{
}
