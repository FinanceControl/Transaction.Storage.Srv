using System.ComponentModel.DataAnnotations;

namespace Transcation.Storage.Srv.Shared.Database.Models;
public abstract class EntityBase
{
  [Key]
  public int Id { get; set; }
}
