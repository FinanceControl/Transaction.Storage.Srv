using Transaction.Storage.Srv.App.Components.AccountComponent.Model;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Dto;

public class CounterPartyTypeDto : ICounterPartyType
{
  public int Id { get; set; }

  public string Name { get; set; }
}