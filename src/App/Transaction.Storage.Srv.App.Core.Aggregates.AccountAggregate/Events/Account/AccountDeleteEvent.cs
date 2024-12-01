using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;
using Transaction.Storage.Srv.Shared.Events;
namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events.AccountEvents;

public class AccountDeleteEvent : EntityDeleteEvent<AccountDto>
{
}