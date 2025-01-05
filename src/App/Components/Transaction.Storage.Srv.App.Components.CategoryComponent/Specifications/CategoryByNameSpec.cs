using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Entity;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Models;
using Transaction.Storage.Srv.Shared.Database.Specification;

namespace Transaction.Storage.Srv.App.Components.CategoryComponent.Specifications;
public class CategoryByNameSpec :Specification<Category>
{
    public class Factory : UIXSpecificationFactory<Category,ICategoryBody>
    {
        public Factory() : base([nameof(Category.Name)], true)
        {
        }

        public override Specification<Category> Build(ICategoryBody entity)
        {
            return new CategoryByNameSpec(entity);
        }
    }

    public CategoryByNameSpec(string name)
    {
        Query.Where(e => e.Name == name).Take(1);
    }

    public CategoryByNameSpec(ICategoryBody Category) : this(Category.Name)
    {
    }
}