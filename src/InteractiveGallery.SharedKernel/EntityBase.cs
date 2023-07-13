using System.ComponentModel.DataAnnotations.Schema;
using InteractiveGallery.SharedKernel;

/*namespace InteractiveGallery.SharedKernel;
// This can be modified to EntityBase<TId> to support multiple key types (e.g. Guid)
public abstract class EntityBase 
{
  protected EntityBase(int id) { 
  Id = id; 
  }
  public int Id { get; set; }

  //public static bool operator  ==(EntityBase? first,EntityBase? second)
  //{
  //  return first is not null && second is not null && first.Equals(second);
  //}

  //public static bool operator !=(EntityBase? first, EntityBase? second) {
  //  return !(first == second);
  //}
  //public override bool Equals(object? obj)
  //{
  //  if (obj == null) {
  //    return false;
  //      }
  //  if (obj.GetType() != GetType()) {
  //    return false;
  //  }
  //  if(obj is not  EntityBase entityBase)
  //  {
  //    return false;
  //  }

  //  return entityBase.Id == Id;
  //}

  //public override int GetHashCode()
  //{
  //  return Id.GetHashCode();
  //}

  //public bool Equals(EntityBase? other)
  //{
  //  if (other == null) { return false; }
  //  if (other.GetType() != GetType()) { return false; }
  //  return other.Id == Id;
  //} 


  private List<DomainEventBase> _domainEvents = new();
  [NotMapped]
  public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

  protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
  internal void ClearDomainEvents() => _domainEvents.Clear();

}*/

namespace InteractiveGallery.SharedKernel;
// This can be modified to EntityBase<TId> to support multiple key types (e.g. Guid)
public abstract class EntityBase
{
  public int Id { get; set; }

  private List<DomainEventBase> _domainEvents = new();
  [NotMapped]
  public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

  protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
  internal void ClearDomainEvents() => _domainEvents.Clear();
}
