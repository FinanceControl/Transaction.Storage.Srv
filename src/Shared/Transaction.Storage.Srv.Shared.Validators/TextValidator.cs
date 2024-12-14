using FluentValidation;
namespace Transaction.Storage.Srv.Shared.Validators;

public class TextValidator : AbstractValidator<string>
{ 
  public const string DefaultCode = "IsShort";
  public const Severity DefaultSeverity = Severity.Error;
  public TextValidator(string fieldName, int maxLenght = 255 , string code = DefaultCode, Severity severity = DefaultSeverity)
  {
    RuleFor(e => e.Length)
      .LessThanOrEqualTo(maxLenght).WithMessage($"{fieldName} must be less then {maxLenght}").WithErrorCode(code).WithSeverity(severity);
  }
}