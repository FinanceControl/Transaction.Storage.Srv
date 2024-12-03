
using Transaction.Storage.Srv.App.Components.AccountComponent.Model;
using Transaction.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
public class CounterPartyType : EnumEntity<ICounterPartyType.Enum>, ICounterPartyType
{
  public new const int NameLenght = 15;

  public CounterPartyType(int id) : base(id)
  {
  }

  public CounterPartyType(ICounterPartyType.Enum Id) : base(Id)
  {
  }

  

  public ICollection<CounterParty> CounterParties { get; }
}