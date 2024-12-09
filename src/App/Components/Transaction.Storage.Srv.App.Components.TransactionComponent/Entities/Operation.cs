using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Ardalis.Result;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Entity;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Entity;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Models;
using Transaction.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Entity;
public partial class Operation : DomainEntity, IOperation
{
    [MaxLength(50)]
    public string ExternalId { get; set; }
    [Required]
    public DateTime PlanDatetime { get; set; }

    public DateTime CommitDateTime { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }

    [Required]
    public int AccountId { get; set; }

    [Required]
    public int BudgetId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public int AssetId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [MaxLength(50)]
    public string Source { get; set; }
    [MaxLength(255)]
    public string Notes { get; set; }

    public Account? Account { get; set; }
    public Budget? Budget { get; set; }
    public Category? Category { get; set; }
    public Asset? Asset { get; set; }
}