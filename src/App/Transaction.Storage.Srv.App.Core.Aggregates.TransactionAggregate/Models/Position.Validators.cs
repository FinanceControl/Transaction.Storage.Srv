using FluentValidation;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

public partial class Position
{
  public class Validator : AbstractValidator<Position>
  {
    public Validator()
    {
      RuleFor(e => e.Amount).SetValidator(new AmountValidator());
    }
  }

  public class AmountValidator : AbstractValidator<int>
  {
    public AmountValidator()
    {
      RuleFor(e => e).NotEqual(0).WithMessage("Amount counln't be 0");
    }
  }
}