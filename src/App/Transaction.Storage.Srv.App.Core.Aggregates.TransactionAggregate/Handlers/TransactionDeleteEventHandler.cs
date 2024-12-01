using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;
using Transaction.Storage.Srv.Shared.Events.Handlers;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers;

public class TransactionDeleteEventHandler : OldEntityDeleteEventHandler<TransactionDeleteEvent, Header, TransactionDto>
{

  public TransactionDeleteEventHandler(IRepositoryBase<Header> hearderRep) : base(hearderRep)
  {
  }

}