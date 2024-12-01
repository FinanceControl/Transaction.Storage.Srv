using Ardalis.Specification;
using FluentValidation;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

public partial class Header
{
  public class ValidatorList : AbstractValidator<List<INewHeaderDto>>
  {
    public ValidatorList()
    {
      RuleForEach(e => e).SetValidator(new Validator());
    }
  }

  public class Validator : AbstractValidator<INewHeaderDto>
  {
    public Validator()
    {
      RuleFor(e => e.Description).SetValidator(new TextValidator(nameof(Description), DescriptionMaxLenght));
    }
  }

  public class EventValidator : AbstractValidator<TransactionAddEvent>
  {
    public EventValidator(IReadRepositoryBase<Account> accountReadRep, IReadRepositoryBase<Asset> assetReadRep)
    {
      RuleFor(e => e.Header).SetValidator(new Validator());
      RuleForEach(e => e.Positions).SetValidator(new Position.Validator(accountReadRep, assetReadRep));
    }
  }
}