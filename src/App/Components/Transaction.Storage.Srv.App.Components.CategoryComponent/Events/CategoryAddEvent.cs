using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Dto;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Models;

namespace Transaction.Storage.Srv.App.Components.CategoryComponent.Events;

public class CategoryAddEvent: IRequest<Result<CategoryDto>>, ICategoryBody
{
    public string Name {get;set;}
}