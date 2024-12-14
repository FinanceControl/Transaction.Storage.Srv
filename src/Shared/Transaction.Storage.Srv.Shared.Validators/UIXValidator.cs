using Ardalis.Specification;
using FluentValidation;
using Transaction.Storage.Srv.Shared.Database.Specification;

namespace Transaction.Storage.Srv.Shared.Validators;

public class UIXValidator<TEntity,TModel> : AbstractValidator<TModel> where TEntity : class
{
  public const string DefaultCode = "IsDublicated";
  public UIXValidator(IReadRepositoryBase<TEntity> repositoryBase, 
                        IEnumerable<UIXSpecificationFactory<TEntity,TModel>> uix_spec_factory_arr, 
                        string code = DefaultCode)
  {
    foreach (var spec_factory in uix_spec_factory_arr)
    {
        Severity severity = spec_factory.IsUnique ? Severity.Error : Severity.Info;
        RuleFor(e=>e).MustAsync(async (entity, cancellationToken) =>
        {
            var spec = spec_factory.Build(entity);
            var existingEntity = await repositoryBase.FirstOrDefaultAsync(spec, cancellationToken);
            return existingEntity == null; // Убедитесь, что сущность не существует
        })
        .WithMessage($"Has exist entity with same {string.Join(", ", spec_factory.ColumnNames)}")
        .WithErrorCode(code).WithSeverity(severity);
    }
  }
}