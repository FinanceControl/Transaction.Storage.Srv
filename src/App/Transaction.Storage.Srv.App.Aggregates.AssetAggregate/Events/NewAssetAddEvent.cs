using System.ComponentModel.DataAnnotations;
using InsonusK.Shared.Validation;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dto;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;

public class NewAssetAddEvent: IRequest<AssetDto>
{
  public string Name { get; private set; }
  public short DecimalSize { get; private set; }
  public int AssetTypeId { get; private set; }
}