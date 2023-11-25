using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using InsonusK.Shared.Validation;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dto;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;

public class NewAssetTypeAddEvent : IRequest<Result<AssetTypeDto>>
{
  public string Name { get; set; }
  public bool IsInflationProtected { get; set; }
  public bool IsUnderManagement { get; set; }
}
