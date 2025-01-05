using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Models;

public interface IAssetTypeDto : IConstantDomainModelId, IAssetTypeBody
{
}

public interface IAssetTypeBody {
  public string Name { get; }
  public bool IsInflationProtected { get;  }

  public bool IsUnderManagement { get;  }
}