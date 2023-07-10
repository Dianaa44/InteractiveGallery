
namespace InteractiveGallery.SharedKernel;
public abstract class AggregateRoot:EntityBase
{
  private readonly List<DomainEventBase> _domainEventBases = new();
  protected AggregateRoot(int id)
    :base(id) 
  {
  }
  protected void RaiseDomainEvent(DomainEventBase domainEvent)
  {
    _domainEventBases.Add(domainEvent);
  }
}

