using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Specifications;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Handlers;

//public class AccountCheckDependencyEventHandler : IRequestHandler<AccountCheckDependencyEvent, Result>
//{
//  private readonly IReadRepositoryBase<Position> positionRep;
//
//  public AccountCheckDependencyEventHandler(IReadRepositoryBase<Position> positionRep)
//  {
//    this.positionRep = positionRep;
//  }
//  public async Task<Result> Handle(AccountCheckDependencyEvent request, CancellationToken cancellationToken)
//  {
//    var pos_with_acc =  await positionRep.ListAsync(new PositionsByAccountIdSpec(request.Id), cancellationToken);
//    if (pos_with_acc.Count > 0)
//      return Result.Conflict(pos_with_acc.Select(e=>$"Transation Header id {e.HeaderId} Position id {e.Id} related to account").ToArray());
//      
//    return Result.Success();      
//  }
//}