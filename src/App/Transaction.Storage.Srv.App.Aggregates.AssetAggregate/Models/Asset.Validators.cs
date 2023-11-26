using FluentValidation;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

public partial class Asset
{
  public class Validator : AbstractValidator<Asset>
  {
    public Validator()
    {
      RuleFor(at => at.DecimalSize).SetValidator(new DecimalSizeValidator());
      RuleFor(e => e.Name).SetValidator(new NameValidator(NameMaxLenght));
    }
  }

  public class DecimalSizeValidator : AbstractValidator<short>
  {
    public DecimalSizeValidator()
    {
      RuleFor(ds => ds).GreaterThanOrEqualTo((short)0).WithErrorCode("Asset.DecimalSize.001").WithMessage("DecimalSize must be greater or equal 0");
    }
  }
}