using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Component.CategoryComponent.Models;

public interface ICategory : IDomainModel, ICategoryBody
{

}
public interface ICategoryBody
{
    public string Name { get; set; }
}