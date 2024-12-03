using System.ComponentModel.DataAnnotations;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Models;
using Transaction.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Components.CategoryComponent.Entities;

public partial class Category : DomainEntity, ICategory
{
    [Required]
    [MaxLength(50)]
    public string Name {get;set;}
}