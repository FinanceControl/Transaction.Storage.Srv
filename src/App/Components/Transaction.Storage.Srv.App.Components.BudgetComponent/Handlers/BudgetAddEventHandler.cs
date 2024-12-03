using Ardalis.Specification;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Entity;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Events;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Dto;
using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Transaction.Storage.Srv.App.Components.BudgetComponent.Handlers;

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