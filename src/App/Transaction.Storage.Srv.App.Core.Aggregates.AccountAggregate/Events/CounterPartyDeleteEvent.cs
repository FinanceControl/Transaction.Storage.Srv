using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
using Transaction.Storage.Srv.Shared.Events;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;


public class CounterPartyDeleteEvent : EntityDeleteEvent<CounterPartyDto>
{
}