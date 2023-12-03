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
    public Validator(IReadRepositoryBase<Account> accountReadRep, IReadRepositoryBase<Asset> assetReadRep)
    {
      RuleFor(at => at.AccountId).SetValidator(new IdNullableValidator<Account>(accountReadRep, nameof(AccountId)));
      RuleFor(at => at.AssetId).SetValidator(new IdValidator<Asset>(assetReadRep, nameof(AssetId)));
      RuleFor(e => e.Amount).SetValidator(at => new AmountValidator(assetReadRep, at.AssetId));

    }
  }

  public class AmountValidator : AbstractValidator<decimal>
  {
    public AmountValidator(IReadRepositoryBase<Asset> assetReadRep, int assetId)
    {
      var res =
      RuleFor(e => e)
        .NotEqual(0).WithMessage("Amount counln't be 0")
        .WhenAsync(async (e, ct) => await assetReadRep.GetByIdAsync(assetId, ct) != null)
          .MustAsync(async (e, ct) => (await assetReadRep.GetByIdAsync(assetId, ct))!.checkDecimalLenght(e).IsSuccess)
          .WithMessage("Decimal lenght is too long for Asset");
    }
  }
}