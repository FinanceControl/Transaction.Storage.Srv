using FluentValidation;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

public partial class CounterParty
{
  public class Validator : AbstractValidator<CounterParty>
  {
    public Validator()
    {
      RuleFor(e => e.Name).SetValidator(new NameValidator(NameMaxLenght));
    }
  }

}