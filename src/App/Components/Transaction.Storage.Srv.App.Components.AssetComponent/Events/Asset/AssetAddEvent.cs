using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
using Transaction.Storage.Srv.App.Components.AssetComponent.Models;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Events;

public class AssetAddEvent : IRequest<Result<AssetDto>>,IAssetBodyDto
{
  public string Name { get; set; }
  public int AssetTypeId { get; set; }
}
