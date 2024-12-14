using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
using Transaction.Storage.Srv.App.Components.AssetComponent.Models;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Events;

public class AssetTypeAddEvent : IRequest<Result<AssetTypeDto>>, IAssetTypeBody
{
  public string Name { get; set; }
  public bool IsInflationProtected { get; set; }
  public bool IsUnderManagement { get; set; }
}
