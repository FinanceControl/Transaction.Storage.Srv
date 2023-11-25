using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Transcation.Storage.Srv.Shared.Database;
public abstract class EntityBase
{
  [Key]
  public int Id { get; set; }

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
