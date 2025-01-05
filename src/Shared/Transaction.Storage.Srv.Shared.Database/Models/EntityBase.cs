using System.ComponentModel.DataAnnotations;

namespace Transaction.Storage.Srv.Shared.Database.Models;
public abstract class EntityBase
{
  [Key]
  public int Id { get; set; }
}
