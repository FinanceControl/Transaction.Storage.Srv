using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

public partial class Header
{
  public class Factory : IEntityFactory<TransactionAddEvent, Header>
  {
    private readonly IEntityFactory<IPositionBodyDto, Position> posFactory;
    private readonly IReadRepositoryBase<Account> accountReadRep;
    private readonly IReadRepositoryBase<Asset> assetReadRep;

    public Factory(IEntityFactory<IPositionBodyDto, Position> posFactory,
                      IReadRepositoryBase<Account> accountReadRep,
                      IReadRepositoryBase<Asset> assetReadRep)
    {
      this.posFactory = posFactory;
      this.accountReadRep = accountReadRep;
      this.assetReadRep = assetReadRep;
    }
    public async Task<Result<Header>> BuildAsync(TransactionAddEvent source, CancellationToken cancellationToken = default)
    {
      var source_result = await new EventValidator(accountReadRep, assetReadRep).ValidateAsync(source);
      if (!source_result.IsValid)
        return Result.Invalid(source_result.AsErrors());

      List<Position> newPosList = new List<Position>();

      foreach (var pos in source.Positions)
      {
        var newPos = await posFactory.BuildAsync(pos, cancellationToken);
        Guard.Against.Null(newPos.Value);

        newPosList.Add(newPos.Value);
      }

      var newHeader = new Header(source.Header)
      {
        Positions = newPosList
      };

      return Result.Success(newHeader);
    }


  }
  protected Header()
  {
  }

  protected Header(INewHeaderDto newHeaderDto)
  {
    Description = newHeaderDto.Description;
    CommitDateTime = newHeaderDto.CommitDateTime;
  }
}