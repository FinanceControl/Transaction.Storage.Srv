
using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Handlers;

public class TransactionAddEventHandler : EntityAddEventHandler<TransactionAddEvent, Header, TransactionDto>
{
  private readonly IReadRepositoryBase<Account> _accountReadRep;
  private readonly IReadRepositoryBase<Asset> _assetReadRep;

  public TransactionAddEventHandler(IRepositoryBase<Header> headerRep,
                                  IEntityFactory<TransactionAddEvent, Header> entityFactory,
                                  IReadRepositoryBase<Account> accountReadRep,
                                  IReadRepositoryBase<Asset> assetReadRep) : base(headerRep, entityFactory)
  {
    this._accountReadRep = accountReadRep;
    this._assetReadRep = assetReadRep;
  }
  protected override async Task<Result> CheckDependency(TransactionAddEvent request, CancellationToken cancellationToken)
  {
    List<string> errors = new List<string>();
    foreach (var pos in request.Positions)
    {
      if (pos.AccountId != null)
      {
        var acc = await _accountReadRep.GetByIdAsync((int)pos.AccountId, cancellationToken);
        if (acc is null)
          errors.Add($"AccountId {pos.AccountId} doesn't exist");
      }

      var asset = await _assetReadRep.GetByIdAsync(pos.AssetId, cancellationToken);
      if (asset is null)
        errors.Add($"AssetId {pos.AssetId} doesn't exist");
    }


    if (errors.Count > 0)
      return Result.NotFound(errors.ToArray());

    return await base.CheckDependency(request, cancellationToken);
  }

  protected override Task<TransactionDto> ToResult(Header entity)
  {
    return Task.FromResult(new TransactionDto()
    {
      Header = entity.Adapt<HeaderDto>(),
      Positions = entity.Positions.Select(p => p.Adapt<PositionDto>()).ToList()
    });
  }
}