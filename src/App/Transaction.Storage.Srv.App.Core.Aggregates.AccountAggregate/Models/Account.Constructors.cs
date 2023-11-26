
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Mapster;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

public partial class Account
{
  public static Result<Account> BuildNew(AccountAddEvent assetAddEventDto)
  {
    var new_assertType = new Account(assetAddEventDto);
    var result = new Validator().Validate(new_assertType);
    if (result.IsValid)
      return Result.Success(new_assertType);
    else
      return Result.Invalid(result.AsErrors());
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