using Ardalis.Specification;
using Mapster;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Specifications;
public class TransactionByHeaderIdSpec : SingleResultSpecification<Header, TransactionDto>
{
  public TransactionByHeaderIdSpec(int headerId)
  {
    Query.Select(e => new TransactionDto()
    {
      Header = e.Adapt<HeaderDto>(),
      Positions = e.Positions.Select(p => p.Adapt<PositionDto>()).ToList()
    })
    .Include(e => e.Positions)
    .Where(e => e.Id == headerId);
  }
}