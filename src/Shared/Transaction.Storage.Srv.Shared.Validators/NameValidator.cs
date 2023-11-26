using FluentValidation;
namespace Transaction.Storage.Srv.Shared.Validators;

public class NameValidator : AbstractValidator<string>
{
  public NameValidator(int maxLenght = 50, string fieldName = "Name")
  {
    RuleFor(name => name)
      .SetValidator(new TextValidator(fieldName, maxLenght))
      .NotEmpty().WithMessage($"{fieldName} can't be empty")
      .NotNull().WithMessage($"{fieldName} can't be null");
  }
}