using Ardalis.Specification;
using FluentValidation;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

public partial class CounterParty
{
  public class Validator : AbstractValidator<ICounterPartyBodyDto>
  {
    public Validator(IReadRepositoryBase<CounterPartyType> counterPartyTypeRep) : this()
    {
      RuleFor(at => at.CounterPartyTypeId).SetValidator(new IdValidator<CounterPartyType>(counterPartyTypeRep, nameof(CounterPartyTypeId)));
    }
    public Validator()
    {
      RuleFor(e => e.Name).SetValidator(new NameValidator(NameMaxLenght));
    }
  }

}