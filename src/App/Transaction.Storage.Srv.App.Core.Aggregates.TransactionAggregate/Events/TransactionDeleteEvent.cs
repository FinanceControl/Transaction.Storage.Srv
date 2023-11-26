using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;
using Transaction.Storage.Srv.Shared.Events;
namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;

public class TransactionDeleteEvent : EntityDeleteEvent<TransactionDto>
{
}