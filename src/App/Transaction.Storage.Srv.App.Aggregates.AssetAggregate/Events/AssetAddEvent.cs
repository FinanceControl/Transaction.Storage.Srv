using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using InsonusK.Shared.Validation;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dto;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;

public class AssetAddEvent : IRequest<Result<AssetDto>>
{
  public string Name { get; private set; }
  public short DecimalSize { get; private set; }
  public int AssetTypeId { get; private set; }
}
