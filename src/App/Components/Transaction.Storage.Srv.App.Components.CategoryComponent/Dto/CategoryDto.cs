using Transaction.Storage.Srv.App.Components.CategoryComponent.Models;

namespace Transaction.Storage.Srv.App.Components.CategoryComponent.Dto;

public class CategoryDto : NewCategoryDto, ICategory
{
    public Guid Guid {get;}

    public DateTimeOffset CreatedDateTime  {get;}

    public DateTimeOffset UpdatedDateTime  {get;}

    public int Id  {get;}
}
public class NewCategoryDto : ICategoryBody
{
    public string Name {get;set;}
}