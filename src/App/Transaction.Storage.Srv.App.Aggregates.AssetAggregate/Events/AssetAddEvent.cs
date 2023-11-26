using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using InsonusK.Shared.Validation;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;

public class AssetAddEvent : IRequest<Result<AssetDto>>
{
  public string Name { get; set; }
  public short DecimalSize { get; set; }
  public int AssetTypeId { get; set; }
}
