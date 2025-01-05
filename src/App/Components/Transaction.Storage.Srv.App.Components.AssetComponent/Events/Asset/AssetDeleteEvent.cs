using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
using Transaction.Storage.Srv.Shared.Events;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Events;

public class AssetDeleteEvent : EntityDeleteEvent<AssetDto>
{
}