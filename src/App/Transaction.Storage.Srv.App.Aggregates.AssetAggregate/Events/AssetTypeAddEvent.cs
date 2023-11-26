using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using InsonusK.Shared.Validation;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;

public class AssetTypeAddEvent : IRequest<Result<AssetTypeDto>>
{
  public string Name { get; set; }
  public bool IsInflationProtected { get; set; }
  public bool IsUnderManagement { get; set; }
}
