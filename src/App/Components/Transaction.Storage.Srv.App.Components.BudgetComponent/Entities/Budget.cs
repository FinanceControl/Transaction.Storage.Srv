using System.ComponentModel.DataAnnotations;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Models;
using Transaction.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Components.BudgetComponent.Entity;

public partial class Budget : DomainEntity, IBudget
{
    [Required]
    [MaxLength(50)]
    public string Name {get;set;}
}