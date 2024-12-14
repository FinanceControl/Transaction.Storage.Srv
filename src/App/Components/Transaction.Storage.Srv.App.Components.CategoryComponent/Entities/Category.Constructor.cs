using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Events;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Models;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Specifications;
using Transaction.Storage.Srv.Shared.Database.Models;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Components.CategoryComponent.Entity;
public partial class Category : DomainEntity, ICategory
{
    public class Factory : IEntityFactory<CategoryAddEvent, Category>
    {
        private readonly IReadRepositoryBase<Category> entityRep;

        public Factory(IReadRepositoryBase<Category> entityRep)
        {
            this.entityRep = entityRep;
        }

        public async Task<Result<Category>> BuildAsync(CategoryAddEvent source, CancellationToken cancellationToken = default)
        {
            var source_result = await new UIXValidator<Category,ICategoryBody>(entityRep,[new CategoryByNameSpec.Factory()]).ValidateAsync(source);
            
            if (!source_result.IsValid)
                return Result.Conflict(source_result.Errors.Select(e=>e.ErrorMessage).ToArray());

            var new_Budget = new Category(source);
            
            return Result.Success(new_Budget);
        }

    }
    
    protected Category()
    {
    }

    public Category(CategoryAddEvent addEventDto) : base()
    {
        addEventDto.Adapt(this);
    }
}