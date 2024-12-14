using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using Mapster;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events.AccountEvents;
using Transaction.Storage.Srv.App.Components.AccountComponent.Model;
using Transaction.Storage.Srv.App.Components.AccountComponent.Specifications;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Entity;

public partial class Account
{
  public class Factory : IEntityFactory<AccountAddEvent, Account>
  {
    private readonly IReadRepositoryBase<CounterParty> counterPartyRep;
    private readonly IReadRepositoryBase<Account> entityRep;

    public Factory(IReadRepositoryBase<CounterParty> counterPartyRep, IReadRepositoryBase<Account> entityRep)
    {
      this.counterPartyRep = counterPartyRep;
      this.entityRep = entityRep;
    }
    public async Task<Result<Account>> BuildAsync(AccountAddEvent source, CancellationToken cancellationToken = default)
    {
      var source_result = await new Validator(counterPartyRep).ValidateAsync(source);
      if (!source_result.IsValid)
        return Result.Invalid(source_result.AsErrors());

      source_result = await new UIXValidator<Account, IAccountBody>(entityRep, [new AccountByNameSpec.Factory()]).ValidateAsync(source);
      if (!source_result.IsValid)
        return Result.Conflict(source_result.Errors.Select(e => e.ErrorMessage).ToArray());

      var new_assertType = new Account(source);

      return Result.Success(new_assertType);
    }

  }

  protected Account()
  {
  }

  protected Account(AccountAddEvent addEventDto)
  {
    addEventDto.Adapt(this);
  }
}