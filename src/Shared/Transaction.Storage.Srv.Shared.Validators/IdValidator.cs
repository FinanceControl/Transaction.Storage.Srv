using Ardalis.Specification;
using FluentValidation;
using Transaction.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.Shared.Validators;
/// <summary>
/// Validator for Required ID
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class IdValidator<TEntity> : AbstractValidator<int> where TEntity : EntityBase
{
  public const string DefaultIdFieldName = "Id";
  public const string DefaultCode = "IdNotFound";
  public const Severity DefaultSeverity = Severity.Error;

  public IdValidator(IReadRepositoryBase<TEntity> readRep, string fieldName = DefaultIdFieldName, string code = DefaultCode, Severity severity = DefaultSeverity)
  {
    RuleFor(id => id).MustAsync(async (id, ct) => (await readRep.GetByIdAsync(id, ct)) != null)
      .WithMessage((id) => $"{fieldName} {id} doesn't exist")
      .WithErrorCode(code)
      .WithSeverity(severity);
  }
}

/// <summary>
/// Validator for optional ID
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class IdNullableValidator<TEntity> : AbstractValidator<int?> where TEntity : EntityBase
{
  public IdNullableValidator(IReadRepositoryBase<TEntity> readRep, string fieldName = "Id")
  {
    RuleFor(id => id).MustAsync(async (id, ct) => (await readRep.GetByIdAsync((int)id!, ct)) != null)
      .When(id => id != null)
      .WithMessage((id) => $"{fieldName} {id} doesn't exist");
  }
}