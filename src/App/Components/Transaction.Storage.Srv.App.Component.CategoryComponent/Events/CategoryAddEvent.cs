using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Component.CategoryComponent.Dto;
using Transaction.Storage.Srv.App.Component.CategoryComponent.Models;

namespace Transaction.Storage.Srv.App.Component.CategoryComponent.Events;

public class CategoryAddEvent: IRequest<Result<CategoryDto>>, ICategoryBody
{
    public string Name {get;set;}
}