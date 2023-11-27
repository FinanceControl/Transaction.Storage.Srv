using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Transcation.Storage.Srv.Shared.Database.Models;

public class EventLogEntity : EntityBase
{
  [Required]
  [MaxLength(50)]
  public string Name { get; set; }

  [Required]
  public DateTimeOffset DateTime { get; private set; }

  [Column(TypeName = "jsonb")] // Specify the JSON column type (e.g., json, jsonb in PostgreSQL)
  public string Body { get; set; }

  public static void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<EventLogEntity>()
        .Property(e => e.DateTime)
        .HasConversion(
            v => v,
            v => v.UtcDateTime
        );
  }
}