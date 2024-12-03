using Ardalis.Specification;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Component.CategoryComponent.Dto;
using Transaction.Storage.Srv.App.Component.CategoryComponent.Entities;
using Transaction.Storage.Srv.App.Component.CategoryComponent.Events;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Component.CategoryComponent.Handlers;

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