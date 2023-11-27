using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

public partial class Position
{
  public class Factory : IEntityFactory<IPositionBodyDto, Position>
  {
    private readonly IReadRepositoryBase<Asset> assetRep;
    private readonly IReadRepositoryBase<Account> accountRep;

    public Factory(IReadRepositoryBase<Asset> assetRep, IReadRepositoryBase<Account> accountRep)
    {
      this.assetRep = assetRep;
      this.accountRep = accountRep;
    }
    public async Task<Result<Position>> BuildAsync(IPositionBodyDto source, CancellationToken cancellationToken = default)
    {
      var source_result = await new Validator(accountRep,assetRep).ValidateAsync(source);
      if (!source_result.IsValid)
        return Result.Invalid(source_result.AsErrors());

      var asset = await assetRep.GetByIdAsync(source.AssetId, cancellationToken);
      var account = source.AccountId != null ? await accountRep.GetByIdAsync((int)source.AccountId, cancellationToken) : null;

      Guard.Against.Null(asset);

      var new_assertType = new Position(source)
      {
        Asset = asset,
        Account = account
      };

      return Result.Success(new_assertType);
    }
  }

  protected Position()
  {
  }

  protected Position(IPositionBodyDto newPositionDto)
  {
    AssetId = newPositionDto.AssetId;
    AccountId = newPositionDto.AccountId;
    Amount = newPositionDto.Amount;
  }
}