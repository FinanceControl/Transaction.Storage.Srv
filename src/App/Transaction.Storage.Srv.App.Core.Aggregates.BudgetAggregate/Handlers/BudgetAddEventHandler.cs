using Ardalis.Specification;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.App.Core.Aggregates.BudgetAggregate.Entities;
using Transaction.Storage.App.Core.Aggregates.BudgetAggregate.Events;
using Transaction.Storage.App.Core.Aggregates.BudgetAggregate.Dto;
using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Handlers;

public class BudgetAddEventHandler : EntityAddEventHandler<BudgetAddEvent, Budget, BudgetDto>
{

  public BudgetAddEventHandler(
              IRepositoryBase<Budget> rep,
              IEntityFactory<BudgetAddEvent, Budget> entityFactory,
              ILogger<BudgetAddEventHandler> logger)
          : base(rep, entityFactory, logger)
  {
  }
}