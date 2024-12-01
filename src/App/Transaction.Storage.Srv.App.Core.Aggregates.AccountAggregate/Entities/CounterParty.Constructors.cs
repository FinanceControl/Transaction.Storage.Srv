
using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;

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
      var source_result = await new Validator(counterPartyTypeRep).ValidateAsync(source);
      if (!source_result.IsValid)
        return Result.Invalid(source_result.AsErrors());

      var counterPartyType = await counterPartyTypeRep.GetByIdAsync(source.CounterPartyTypeId, cancellationToken);
      Guard.Against.Null(counterPartyType);

      var new_counterParty = new CounterParty(source)
      {
        CounterPartyType = counterPartyType
      };

      return Result.Success(new_counterParty);
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