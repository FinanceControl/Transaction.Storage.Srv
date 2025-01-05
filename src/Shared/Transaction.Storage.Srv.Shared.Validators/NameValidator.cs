using FluentValidation;
namespace Transaction.Storage.Srv.Shared.Validators;

public class NameValidator : AbstractValidator<string>
{
  public const string DefaultIdFieldName = "Name";
  public const string DefaultCode = "IsNullOrEmpty";
  public const Severity DefaultSeverity = Severity.Error;
  public NameValidator(int maxLenght = 50, string fieldName = DefaultIdFieldName, string code = DefaultCode, Severity severity = DefaultSeverity)
  {
    RuleFor(name => name)
      .SetValidator(new TextValidator(fieldName, maxLenght))
      .NotEmpty().WithMessage($"{fieldName} can't be empty").WithErrorCode(code).WithSeverity(severity)
      .NotNull().WithMessage($"{fieldName} can't be null").WithErrorCode(code).WithSeverity(severity);
  }
}