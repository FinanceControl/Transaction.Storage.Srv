
using Ardalis.GuardClauses;
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
    private readonly IReadRepositoryBase<CounterPartyType> counterPartyTypeRep;

    public Factory(IReadRepositoryBase<CounterPartyType> counterPartyTypeRep)
    {
      this.counterPartyTypeRep = counterPartyTypeRep;
    }
    public async Task<Result<CounterParty>> BuildAsync(CounterPartyAddEvent source, CancellationToken cancellationToken = default)
    {
      var counterPartyType = await counterPartyTypeRep.GetByIdAsync(source.CounterPartyTypeId, cancellationToken);
      Guard.Against.Null(counterPartyType);

      var new_assertType = new CounterParty(source)
      {
        CounterPartyType = counterPartyType
      };
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