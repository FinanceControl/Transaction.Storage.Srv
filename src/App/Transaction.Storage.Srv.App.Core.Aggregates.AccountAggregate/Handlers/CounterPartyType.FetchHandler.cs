using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers;

public class CounterPartyTypeFetchHandler : IRequestHandler<CounterPartyTypeFetchEvent, Result<IEnumerable<CounterPartyTypeDto>>>
{
    private readonly IReadRepositoryBase<CounterPartyType> _rep;
    public CounterPartyTypeFetchHandler(IReadRepositoryBase<CounterPartyType> rep){
        this._rep = rep;
    }
    public async Task<Result<IEnumerable<CounterPartyTypeDto>>> Handle(CounterPartyTypeFetchEvent request, CancellationToken cancellationToken)
    {
        var entities = await _rep.ListAsync(cancellationToken);
        return Result.Success(entities.Adapt<IEnumerable<CounterPartyTypeDto>>());
    }
}