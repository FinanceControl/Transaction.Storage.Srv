using Ardalis.Specification;
using FluentValidation;
using Transcation.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.Shared.Validators;
public class IdValidator<TEntity> : AbstractValidator<int> where TEntity : EntityBase
{
  public IdValidator(IReadRepositoryBase<TEntity> readRep, string fieldName = "Id")
  {
    RuleFor(id => id).MustAsync(async (id, ct) => (await readRep.GetByIdAsync(id, ct)) != null)
      .WithMessage((id) => $"{fieldName} {id} doesn't exist");
  }
}

public class IdNullableValidator<TEntity> : AbstractValidator<int?> where TEntity : EntityBase
{
  public IdNullableValidator(IReadRepositoryBase<TEntity> readRep, string fieldName = "Id")
  {
    RuleFor(id => id).MustAsync(async (id, ct) => (await readRep.GetByIdAsync((int)id!, ct)) != null)
      .When(id => id != null)
      .WithMessage((id) => $"{fieldName} {id} doesn't exist");
  }
}