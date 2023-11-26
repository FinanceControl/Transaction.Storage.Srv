using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Transcation.Storage.Srv.Shared.Database;
public abstract class EntityBase
{
  [Key]
  public int Id { get; set; }

  [Required]
  public DateTimeOffset CreatedDateTime { get; private set; }

  [Required]
  public DateTimeOffset UpdatedDateTime { get; private set; }
}
