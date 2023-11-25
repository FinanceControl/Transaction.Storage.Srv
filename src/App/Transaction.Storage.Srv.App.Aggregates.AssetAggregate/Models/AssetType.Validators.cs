using FluentValidation;
namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

public partial class AssetType
{
  public class Validator : AbstractValidator<AssetType>
  {
    public Validator()
    {
      RuleFor(at => at.Name).SetValidator(new NameValidator());
    }
  }
  
  public class NameValidator : AbstractValidator<string>
  {
    public NameValidator()
    {
      RuleFor(name => name.Length)
        .LessThanOrEqualTo(50).WithErrorCode("AssetType.Name.001").WithMessage("Name must be less then 50")
        .NotEmpty().WithErrorCode("AssetType.Name.002").WithMessage("Name can't be empty")
        .NotNull().WithErrorCode("AssetType.Name.003").WithMessage("Name can't be empty");
    }
  }
}