using Ardalis.Specification;
using FluentValidation;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

public partial class Account
{
  public class Validator : AbstractValidator<IAccountBodyDto>
  {
    public Validator(IReadRepositoryBase<CounterParty> counterPartyRep) : this()
    {
      RuleFor(at => at.CounterPartyId).SetValidator(new IdValidator<CounterParty>(counterPartyRep, nameof(CounterPartyId)));
    }
    public Validator()
    {
      RuleFor(e => e.Name).SetValidator(new NameValidator(NameMaxLenght));
      RuleFor(e => e.Description).SetValidator(new TextValidator(nameof(Description), DescriptionMaxLenght));
    }
  }
}