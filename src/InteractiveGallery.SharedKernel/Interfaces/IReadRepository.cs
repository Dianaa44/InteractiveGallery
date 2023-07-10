using Ardalis.Specification;

namespace InteractiveGallery.SharedKernel.Interfaces;
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : AggregateRoot
{

}
