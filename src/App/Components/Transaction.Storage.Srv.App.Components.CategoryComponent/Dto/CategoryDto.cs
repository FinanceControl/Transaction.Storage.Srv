using Transaction.Storage.Srv.App.Components.CategoryComponent.Models;

namespace Transaction.Storage.Srv.App.Components.CategoryComponent.Dto;

public class CategoryDto : NewCategoryDto, ICategory
{
    public Guid Guid {get;set;}

    public DateTimeOffset CreatedDateTime  {get;set;}

    public DateTimeOffset UpdatedDateTime  {get;set;}

    public int Id  {get;set;}
}
public class NewCategoryDto : ICategoryBody
{
    public string Name {get;set;}
}