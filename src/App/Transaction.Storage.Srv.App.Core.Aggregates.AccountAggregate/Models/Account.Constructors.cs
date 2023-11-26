
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using Mapster;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

public partial class Account
{
  public class Factory : IEntityFactory<AccountAddEvent, Account>
  {

    public Task<Result<Account>> BuildAsync(AccountAddEvent source, CancellationToken cancellationToken = default)
    {
      var new_assertType = new Account(source);
      var result = new Validator().Validate(new_assertType);
      if (result.IsValid)
        return Task.FromResult(Result.Success(new_assertType));
      else
        return Task.FromResult((Result<Account>)Result.Invalid(result.AsErrors()));
    }
  }

  protected Account()
  {
  }

  protected Account(AccountAddEvent addEventDto)
  {
    Name = addEventDto.Name;
    CounterPartyId = addEventDto.CounterPartyId;
    Description = addEventDto.Description;
    IsUnderManagement = addEventDto.IsUnderManagement;
  }
}