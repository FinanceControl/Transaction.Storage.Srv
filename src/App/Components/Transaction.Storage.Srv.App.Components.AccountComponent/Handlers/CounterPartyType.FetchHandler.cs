using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Handlers;

public class CounterPartyTypeFetchHandler : IRequestHandler<CounterPartyTypeFetchEvent, Result<IEnumerable<CounterPartyTypeDto>>>
{
    private readonly IReadRepositoryBase<CounterPartyType> _rep;
    public CounterPartyTypeFetchHandler(IReadRepositoryBase<CounterPartyType> rep){
        _rep = rep;
    }
    public async Task<Result<IEnumerable<CounterPartyTypeDto>>> Handle(CounterPartyTypeFetchEvent request, CancellationToken cancellationToken)
    {
        var entities = await _rep.ListAsync(cancellationToken);
        return Result.Success(entities.Adapt<IEnumerable<CounterPartyTypeDto>>());
    }
}