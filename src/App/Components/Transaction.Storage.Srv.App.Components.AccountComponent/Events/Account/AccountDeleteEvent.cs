using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;
using Transaction.Storage.Srv.Shared.Events;
namespace Transaction.Storage.Srv.App.Components.AccountComponent.Events.AccountEvents;

public class AccountDeleteEvent : EntityDeleteEvent<AccountDto>
{
}