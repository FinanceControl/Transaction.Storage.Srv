using FluentValidation;
using Transaction.Storage.Srv.App.Components.AssetComponent.Models;
using Transaction.Storage.Srv.Shared.Validators;
namespace Transaction.Storage.Srv.App.Components.AssetComponent.Entity;

public partial class AssetType
{
  public class Validator : AbstractValidator<IAssetTypeBody>
  {
    public Validator()
    {
      RuleFor(at => at.Name).SetValidator(new NameValidator(NameMaxLenght));
    }
  }


}