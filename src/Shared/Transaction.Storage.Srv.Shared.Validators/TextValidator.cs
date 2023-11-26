using FluentValidation;
namespace Transaction.Storage.Srv.Shared.Validators;

public class TextValidator : AbstractValidator<string>
{
  public TextValidator(string fieldName, int maxLenght = 255)
  {
    RuleFor(e => e.Length)
      .LessThanOrEqualTo(maxLenght).WithMessage($"{fieldName} must be less then {maxLenght}");
  }
}