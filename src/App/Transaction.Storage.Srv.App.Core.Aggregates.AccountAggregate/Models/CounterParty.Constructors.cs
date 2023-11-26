
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

public partial class CounterParty
{
  public class Factory : IEntityFactory<CounterPartyAddEvent, CounterParty>
  {
    public Result<CounterParty> Build(CounterPartyAddEvent assetAddEventDto)
    {
      var new_assertType = new CounterParty(assetAddEventDto);
      var result = new Validator().Validate(new_assertType);
      if (result.IsValid)
        return Result.Success(new_assertType);
      else
        return Result.Invalid(result.AsErrors());
    }
  }
  protected CounterParty()
  {
  }

  protected CounterParty(CounterPartyAddEvent assetAddEventDto)
  {
    Name = assetAddEventDto.Name;

  }
}