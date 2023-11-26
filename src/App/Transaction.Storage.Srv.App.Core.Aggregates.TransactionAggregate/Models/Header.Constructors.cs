using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transcation.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

public partial class Header
{
  public class Factory : IEntityFactory<TransactionAddEvent, Header>
  {
    private readonly IEntityFactory<INewPositionDto, Position> posFactory;

    public Factory(IEntityFactory<INewPositionDto, Position> posFactory)
    {
      this.posFactory = posFactory;
    }
    public async Task<Result<Header>> BuildAsync(TransactionAddEvent source, CancellationToken cancellationToken = default)
    {

      List<Position> newPosList = new List<Position>();
      foreach (var pos in source.Positions)
      {
        var newPos = await posFactory.BuildAsync(pos, cancellationToken);
        newPosList.Add(newPos);
      }

      var newHeader = new Header(source.Header)
      {
        Positions = newPosList
      };

      var result = new Validator().Validate(newHeader);
      if (result.IsValid)
        return Result.Success(newHeader);
      else
        return Result.Invalid(result.AsErrors());
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