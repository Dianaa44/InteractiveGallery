
namespace InteractiveGallery.SharedKernel;
public abstract class AggregateRoot: EntityBase
{
  private readonly List<DomainEventBase> _domainEventBases = new();
  public AggregateRoot()
  {
  }
  protected void RaiseDomainEvent(DomainEventBase domainEvent)
  {
    _domainEventBases.Add(domainEvent);
  }
}

