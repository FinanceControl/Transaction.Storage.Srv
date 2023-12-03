using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;

public class TransactionUploadEvent : IRequest<Result<Dictionary<int, UploadResponseDto>>>
{
  public class UploadJsonRowDto
  {
    public required NewHeaderDto Header { get; set; }
    public required List<NewPositionDto> Positions { get; set; }
  }
  public required List<UploadJsonRowDto> records { get; set; }
}