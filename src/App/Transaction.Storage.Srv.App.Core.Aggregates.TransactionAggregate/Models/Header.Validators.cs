using FluentValidation;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

public partial class Header
{
  public class Validator : AbstractValidator<Header>
  {
    public Validator()
    {
      RuleFor(e => e.Description).SetValidator(new TextValidator(nameof(Description), DescriptionMaxLenght));
    }
  }
}