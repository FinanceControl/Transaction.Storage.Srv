using Ardalis.Specification;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Dtos;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Entity;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Events;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Handlers;

public class OperationAddEventHandler : EntityAddEventHandler<OperationAddEvent, Operation, OperationDto>
{

    public OperationAddEventHandler(
                IRepositoryBase<Operation> rep,
                IEntityFactory<OperationAddEvent, Operation> entityFactory,
                ILogger<OperationAddEventHandler> logger)
            : base(rep, entityFactory, logger)
    {
    }
}