using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Events;

public class AssetTypeAddEvent : IRequest<Result<AssetTypeDto>>
{
  public string Name { get; set; }
  public bool IsInflationProtected { get; set; }
  public bool IsUnderManagement { get; set; }
}
