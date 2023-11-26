using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Transcation.Storage.Srv.Shared.Database.Interfaces;

namespace Transcation.Storage.Srv.Shared.Database.Models;

public abstract class DomainEntity : EntityBase, IDomainEntityDto
{
  private DateTimeOffset _createdDateTime;
  [Required]
  public DateTimeOffset CreatedDateTime
  {
    get => _createdDateTime; set
    {
      if (this.Id != 0)
        throw new ApplicationException("Cann't set up CreatedDateTime for exist entity");
      _createdDateTime = value;
    }
  }

  [Required]
  public DateTimeOffset UpdatedDateTime { get; set; }

  [Timestamp]
  public byte[]? Version { get; set; }

  private List<EventLogEntity> _domainEvents = new();


  [NotMapped]
  public IEnumerable<EventLogEntity> DomainEvents => _domainEvents.AsReadOnly();

  protected void RegisterDomainEvent(EventLogEntity domainEvent) => _domainEvents.Add(domainEvent);

  internal void ClearDomainEvents() => _domainEvents.Clear();
}
