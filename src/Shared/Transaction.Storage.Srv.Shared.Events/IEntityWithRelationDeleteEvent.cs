namespace Transaction.Storage.Srv.Shared.Events;

public class EntityWithRelationDeleteEvent : EntityDeleteEvent
{
  public bool IsForced { get; set; }
}
