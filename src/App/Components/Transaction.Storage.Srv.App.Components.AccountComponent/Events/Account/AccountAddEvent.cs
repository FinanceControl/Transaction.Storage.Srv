using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Components.AccountComponent.Model;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Events.AccountEvents;


public class AccountAddEvent : IRequest<Result<IAccount>>, IAccountBody
{
  public string Name { get; set; }
  public string Description { get; set; }
  public int CounterPartyId { get; set; }
  public bool IsUnderManagement { get; set; }
  public string KeepassId { get; set; }
}