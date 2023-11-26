using FluentValidation;
using Transaction.Storage.Srv.Shared.Validators;
namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

public partial class AssetType
{
  public class Validator : AbstractValidator<AssetType>
  {
    public Validator()
    {
      RuleFor(at => at.Name).SetValidator(new NameValidator(NameMaxLenght));
    }
  }


}