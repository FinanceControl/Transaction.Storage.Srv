
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

public partial class CounterParty
{
  public static Result<CounterParty> BuildNew(CounterPartyAddEvent assetAddEventDto)
  {
    var new_assertType = new CounterParty(assetAddEventDto);
    var result = new Validator().Validate(new_assertType);
    if (result.IsValid)
      return Result.Success(new_assertType);
    else
      return Result.Invalid(result.AsErrors());
  }
  protected CounterParty()
  {
  }

  protected CounterParty(CounterPartyAddEvent assetAddEventDto)
  {
    Name = assetAddEventDto.Name;

  }
}