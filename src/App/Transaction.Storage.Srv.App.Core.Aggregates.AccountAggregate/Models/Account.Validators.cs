using FluentValidation;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

public partial class Account
{
  public class Validator : AbstractValidator<Account>
  {
    public Validator()
    {
      RuleFor(e => e.Name).SetValidator(new NameValidator(NameMaxLenght));
      RuleFor(e => e.Description).SetValidator(new TextValidator(nameof(Description), DescriptionMaxLenght));
    }
  }
}