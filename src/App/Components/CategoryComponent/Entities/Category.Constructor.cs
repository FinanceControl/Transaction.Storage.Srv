using Ardalis.Result;
using Transaction.Storage.Srv.App.Component.CategoryComponent.Events;
using Transaction.Storage.Srv.App.Component.CategoryComponent.Models;
using Transaction.Storage.Srv.Shared.Database.Models;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Component.CategoryComponent.Entities;
public partial class Category : DomainEntity, ICategory
{
    public class Factory : IEntityFactory<CategoryAddEvent, Category>
    {
        public async Task<Result<Category>> BuildAsync(CategoryAddEvent source, CancellationToken cancellationToken = default)
        {
            var new_Budget = new Category(source);
            return Result.Success(new_Budget);
        }

    }
    
    protected Category()
    {
    }

    public Category(CategoryAddEvent addEventDto) : base()
    {
        Name = addEventDto.Name;
    }
}