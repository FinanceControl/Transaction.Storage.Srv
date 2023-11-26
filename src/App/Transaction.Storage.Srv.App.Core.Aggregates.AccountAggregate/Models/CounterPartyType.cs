
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;
using Transcation.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;
public class CounterPartyType : EnumEntity<CounterPartyType.Enum>, ICounterPartyTypeDto
{
  public new const int NameLenght = 15;

  public CounterPartyType(int id) : base(id)
  {
  }

  public CounterPartyType(Enum Id) : base(Id)
  {
  }

  public enum Enum
  {
    LegalEntity = 1,
    Individual = 2,
    Storage = 3
  }

  public ICollection<CounterParty> CounterParties { get; }
}