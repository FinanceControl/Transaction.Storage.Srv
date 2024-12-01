using Transaction.Storage.Srv.Shared.Database.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

public interface IAssetTypeDto : IDomainEntityDto
{
  public string Name { get; }
  public bool IsInflationProtected { get;  }

  public bool IsUnderManagement { get;  }
}