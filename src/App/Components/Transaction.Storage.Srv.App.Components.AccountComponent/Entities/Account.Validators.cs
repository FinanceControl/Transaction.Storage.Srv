using Ardalis.Specification;
using FluentValidation;
using Transaction.Storage.Srv.App.Components.AccountComponent.Model;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Entity;

public partial class Account
{
  public class Validator : AbstractValidator<IAccountBody>
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