using Ardalis.Specification;
using FluentValidation;
using Transaction.Storage.Srv.App.Components.AssetComponent.Models;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Entity;

public partial class Asset
{
  public class Validator : AbstractValidator<IAssetBodyDto>
  {
    public Validator(IReadRepositoryBase<AssetType> assetTypeRep) : this()
    {
      RuleFor(at => at.AssetTypeId).SetValidator(new IdValidator<AssetType>(assetTypeRep, nameof(AssetTypeId)));
    }

    public Validator()
    {
      RuleFor(e => e.Name).SetValidator(new NameValidator(NameMaxLenght));
    }
  }

  public class DecimalSizeValidator : AbstractValidator<short>
  {
    public DecimalSizeValidator()
    {
      RuleFor(ds => ds)
        .GreaterThanOrEqualTo((short)0).WithErrorCode("Asset.DecimalSize.001").WithMessage("DecimalSize must be greater or equal 0")
        .LessThanOrEqualTo((short)15).WithErrorCode("Asset.DecimalSize.002").WithMessage("DecimalSize must be less or equal 15");
    }
  }
}