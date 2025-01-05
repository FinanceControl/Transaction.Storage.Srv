using Ardalis.Specification;
using FluentValidation;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.OperationComponent.Specifications;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Entity;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Models;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Validators;

public class ExternalIdValidator : AbstractValidator<IExternalIdData>
{
    public ExternalIdValidator(IReadRepositoryBase<Account> accountRep,
                               IReadRepositoryBase<Operation> operationRep)
    {

        RuleFor(at => at.ExternalId)
        .MustAsync(async (operationDto, externalId, cancelationToken) =>
        {
            var spec = new OperationByExternalIdSpec(externalId, operationDto.AccountId);
            return (await operationRep.FirstOrDefaultAsync(spec, cancelationToken)) == null;
        })
        .When(dto => !string.IsNullOrEmpty(dto.ExternalId))
        .WithMessage((dto, externalId) => $"Transaction with external Id {externalId} in account Id {dto.AccountId} Name {accountRep.GetByIdAsync(dto.AccountId).Result!.Name}");
    }
}
