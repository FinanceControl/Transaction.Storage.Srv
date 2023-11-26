using FluentValidation;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Validators;
public class NameValidator : AbstractValidator<string>
{
  public NameValidator()
  {
    RuleFor(name => name.Length)
      .LessThanOrEqualTo(50).WithErrorCode("Name.001").WithMessage("Name must be less then 50")
      .NotEmpty().WithErrorCode("Name.002").WithMessage("Name can't be empty")
      .NotNull().WithErrorCode("Name.003").WithMessage("Name can't be null");
  }
}