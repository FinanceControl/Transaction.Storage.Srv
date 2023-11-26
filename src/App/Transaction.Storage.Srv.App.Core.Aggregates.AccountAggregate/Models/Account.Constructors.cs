
using Ardalis.GuardClauses;
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
    private readonly IReadRepositoryBase<CounterParty> counterPartyRep;

    public Factory(IReadRepositoryBase<CounterParty> counterPartyRep)
    {
      this.counterPartyRep = counterPartyRep;
    }
    public async Task<Result<Account>> BuildAsync(AccountAddEvent source, CancellationToken cancellationToken = default)
    {
      var counterParty = await counterPartyRep.GetByIdAsync(source.CounterPartyId, cancellationToken);
      Guard.Against.Null(counterParty);

      var new_assertType = new Account(source)
      {
        CounterParty = counterParty
      };

      var result = new Validator().Validate(new_assertType);
      if (result.IsValid)
        return Result.Success(new_assertType);
      else
        return (Result<Account>)Result.Invalid(result.AsErrors());
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