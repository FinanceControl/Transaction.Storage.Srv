using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

public interface IAssetTypeDto : IConstantDomainModel
{
  public string Name { get; }
  public bool IsInflationProtected { get;  }

  public bool IsUnderManagement { get;  }
}