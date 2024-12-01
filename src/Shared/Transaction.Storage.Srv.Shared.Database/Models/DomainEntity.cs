using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Transaction.Storage.Srv.Shared.Database.Interfaces;
using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.Shared.Database.Models;

public abstract class OldDomainEntity : EntityBase, IDomainEntityDto
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

public abstract class DomainEntity : ConstantDomainEntity, IDomainModel
{

  [Required]
  public Guid Guid {get;set;}
  
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
}

public abstract class ConstantDomainEntity : EntityBase, IConstantDomainModel
{

  private List<EventLogEntity> _domainEvents = new();


  [NotMapped]
  public IEnumerable<EventLogEntity> DomainEvents => _domainEvents.AsReadOnly();

  protected void RegisterDomainEvent(EventLogEntity domainEvent) => _domainEvents.Add(domainEvent);

  internal void ClearDomainEvents() => _domainEvents.Clear();
}
