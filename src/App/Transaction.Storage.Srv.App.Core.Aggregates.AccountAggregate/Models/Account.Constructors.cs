
using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
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
      var source_result = await new Validator(counterPartyRep).ValidateAsync(source);
      if (!source_result.IsValid)
        return Result.Invalid(source_result.AsErrors());

      var counterParty = await counterPartyRep.GetByIdAsync(source.CounterPartyId, cancellationToken);
      Guard.Against.Null(counterParty);

      var new_account = new Account(source)
      {
        CounterParty = counterParty
      };

      return Result.Success(new_account);
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