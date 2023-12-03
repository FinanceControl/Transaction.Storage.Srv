using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Specifications;
public class PositionsByAssetIdSpec : SingleResultSpecification<Position>
{
  public PositionsByAssetIdSpec(int assetId)
  {
    Query.Where(e => e.AssetId == assetId);
  }
}