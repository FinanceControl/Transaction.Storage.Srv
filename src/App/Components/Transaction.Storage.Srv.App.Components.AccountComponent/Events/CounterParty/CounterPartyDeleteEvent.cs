using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;
using Transaction.Storage.Srv.Shared.Events;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Events.CounterPartyEvents;


public class CounterPartyDeleteEvent : EntityDeleteEvent<CounterPartyDto>
{
}