using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Interfaces;
using Transcation.Storage.Srv.Shared.Database.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;

public class AssetUpdateEvent : IRequest<Result<AssetDto>>, IEntityBaseDto, IAssetBodyDto
{
  public string Name { get; set; }
  public short DecimalSize { get; set; }
  public int AssetTypeId { get; set; }

  public int Id { get; set; }
}
