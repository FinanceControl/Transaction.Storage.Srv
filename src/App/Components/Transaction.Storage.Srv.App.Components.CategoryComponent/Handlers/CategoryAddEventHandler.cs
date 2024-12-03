using Ardalis.Specification;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Dto;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Entities;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Events;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Components.CategoryComponent.Handlers;

public class CategoryAddEventHandler : EntityAddEventHandler<CategoryAddEvent, Category, CategoryDto>
{

  public CategoryAddEventHandler(
              IRepositoryBase<Category> rep,
              IEntityFactory<CategoryAddEvent, Category> entityFactory,
              ILogger<CategoryAddEventHandler> logger)
          : base(rep, entityFactory, logger)
  {
  }
}