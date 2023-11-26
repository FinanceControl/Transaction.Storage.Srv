using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.Metadata;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

public partial class Position
{
  public class Factory : IEntityFactory<INewPositionDto, Position>
  {
    private readonly IReadRepositoryBase<Asset> assetRep;
    private readonly IReadRepositoryBase<Account> accountRep;

    public Factory(IReadRepositoryBase<Asset> assetRep, IReadRepositoryBase<Account> accountRep)
    {
      this.assetRep = assetRep;
      this.accountRep = accountRep;
    }
    public async Task<Result<Position>> BuildAsync(INewPositionDto source, CancellationToken cancellationToken = default)
    {
      var asset = await assetRep.GetByIdAsync(source.AssetId, cancellationToken);
      var account = source.AccountId != null ? await accountRep.GetByIdAsync((int)source.AccountId, cancellationToken) : null;

      Guard.Against.Null(asset);

      var new_assertType = new Position(source)
      {
        Asset = asset,
        Account = account
      };

      var result = new Validator().Validate(new_assertType);
      if (result.IsValid)
        return Result.Success(new_assertType);
      else
        return Result.Invalid(result.AsErrors());
    }
  }

  protected Position()
  {
  }

  protected Position(INewPositionDto newPositionDto)
  {
    AssetId = newPositionDto.AssetId;
    AccountId = newPositionDto.AccountId;
    Amount = newPositionDto.Amount;
  }
}