using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Transcation.Storage.Srv.Shared.Database.Interfaces;

namespace Transcation.Storage.Srv.Shared.Database.Models;

public abstract class DomainEntity : EntityBase,IDomainEntityDto
{
  [Required]
  public DateTimeOffset CreatedDateTime { get; private set; }

  [Required]
  public DateTimeOffset UpdatedDateTime { get; private set; }
  private List<EventLogEntity> _domainEvents = new();
  [NotMapped]
  public IEnumerable<EventLogEntity> DomainEvents => _domainEvents.AsReadOnly();

  protected void RegisterDomainEvent(EventLogEntity domainEvent) => _domainEvents.Add(domainEvent);

  internal void ClearDomainEvents() => _domainEvents.Clear();
}
