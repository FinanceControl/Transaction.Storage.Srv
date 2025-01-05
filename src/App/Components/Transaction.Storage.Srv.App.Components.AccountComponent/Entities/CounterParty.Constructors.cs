using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using Mapster;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events.CounterPartyEvents;
using Transaction.Storage.Srv.App.Components.AccountComponent.Model;
using Transaction.Storage.Srv.App.Components.AccountComponent.Specifications;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Entity;

public partial class CounterParty
{
  public class Factory : IEntityFactory<CounterPartyAddEvent, CounterParty>
  {
    private readonly IReadRepositoryBase<CounterPartyType> counterPartyTypeRep;
    private readonly IReadRepositoryBase<CounterParty> entityRep;

    public Factory(IReadRepositoryBase<CounterPartyType> counterPartyTypeRep, IReadRepositoryBase<CounterParty> entityRep)
    {
      this.counterPartyTypeRep = counterPartyTypeRep;
      this.entityRep = entityRep;
    }

    public async Task<Result<CounterParty>> BuildAsync(CounterPartyAddEvent source, CancellationToken cancellationToken = default)
    {
      var source_result = await new Validator(counterPartyTypeRep).ValidateAsync(source);
      if (!source_result.IsValid)
        return Result.Invalid(source_result.AsErrors());

      source_result = await new UIXValidator<CounterParty, ICounterPartyBody>(entityRep, [new CounterPartyByNameSpec.Factory()]).ValidateAsync(source);
      if (!source_result.IsValid)
        return Result.Conflict(source_result.Errors.Select(e => e.ErrorMessage).ToArray());

      var new_assertType = new CounterParty(source);

      return Result.Success(new_assertType);
    }
  }
  protected CounterParty()
  {
  }

  protected CounterParty(CounterPartyAddEvent addEventDto)
  {
    addEventDto.Adapt(this);

  }
}