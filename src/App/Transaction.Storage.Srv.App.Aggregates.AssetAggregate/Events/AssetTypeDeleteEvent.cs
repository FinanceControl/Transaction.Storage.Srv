using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.Shared.Events;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;

public class AssetTypeDeleteEvent : EntityDeleteEvent<AssetTypeDto>
{
}
