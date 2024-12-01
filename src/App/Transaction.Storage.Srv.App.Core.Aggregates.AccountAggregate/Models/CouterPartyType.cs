
using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Model;

public interface ICounterPartyType : IEnumModel
{
    public enum Enum
    {
        Company = 1,
        Individual = 2,
        Storage = 3
    }
}
