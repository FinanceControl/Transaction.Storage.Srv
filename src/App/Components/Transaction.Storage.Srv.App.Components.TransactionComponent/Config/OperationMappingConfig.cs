using Mapster;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Entity;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Events;
namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Configs;

public class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<OperationAddEvent, Operation>
            .NewConfig()
            .IgnoreNullValues(true) // Игнорировать null значения
            .PreserveReference(true); // Сохранять ссылки, если требуется
    }
}