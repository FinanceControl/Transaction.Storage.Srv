using Ardalis.Specification;
using FluentValidation;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

public partial class Position
{
  public class Validator : AbstractValidator<IPositionBodyDto>
  {
    public Validator(IReadRepositoryBase<Account> accountReadRep, IReadRepositoryBase<Asset> assetReadRep) : this()
    {
      RuleFor(at => at.AccountId).SetValidator(new IdNullableValidator<Account>(accountReadRep, nameof(AccountId)));
      RuleFor(at => at.AssetId).SetValidator(new IdValidator<Asset>(assetReadRep, nameof(AssetId)));
    }
    public Validator()
    {
      RuleFor(e => e.Amount).SetValidator(new AmountValidator());
    }
  }

  public class AmountValidator : AbstractValidator<int>
  {
    public AmountValidator()
    {
      RuleFor(e => e).NotEqual(0).WithMessage("Amount counln't be 0");
    }
  }
}