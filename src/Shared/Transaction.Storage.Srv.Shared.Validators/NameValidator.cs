using FluentValidation;
namespace Transaction.Storage.Srv.Shared.Validators;

public class NameValidator : AbstractValidator<string>
{
  public NameValidator(int maxLenght = 50)
  {
    RuleFor(name => name.Length)
      .LessThanOrEqualTo(maxLenght).WithErrorCode("Name.001").WithMessage("Name must be less then 50")
      .NotEmpty().WithErrorCode("Name.002").WithMessage("Name can't be empty")
      .NotNull().WithErrorCode("Name.003").WithMessage("Name can't be null");
  }
}