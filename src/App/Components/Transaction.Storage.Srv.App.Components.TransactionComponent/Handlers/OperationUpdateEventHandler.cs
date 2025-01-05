using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Entity;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Entity;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Dtos;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Entity;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Events;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Handlers;

public class OperationUpdateEventHandler : IRequestHandler<OperationUpdateEvent, Result<OperationDto>>
{
    private readonly ILogger<OperationUpdateEventHandler> _logger;
    private readonly IRepositoryBase<Operation> repository;
    private readonly IReadRepositoryBase<Account> accountRep;
    private readonly IReadRepositoryBase<Asset> assetRep;
    private readonly IReadRepositoryBase<Category> categoryRep;
    private readonly IReadRepositoryBase<Budget> budgetRep;
    private readonly IReadRepositoryBase<Operation> operationRep;

    public OperationUpdateEventHandler( ILogger<OperationUpdateEventHandler> logger,
                                        IRepositoryBase<Operation> repository,
                                        IReadRepositoryBase<Account> accountRep,
                                        IReadRepositoryBase<Asset> assetRep,
                                        IReadRepositoryBase<Category> categoryRep,
                                        IReadRepositoryBase<Budget> budgetRep,
                                        IReadRepositoryBase<Operation> operationRep)
    {
        _logger = logger;
        this.repository = repository;
        this.accountRep = accountRep;
        this.assetRep = assetRep;
        this.categoryRep = categoryRep;
        this.budgetRep = budgetRep;
        this.operationRep = operationRep;
    }

    public async Task<Result<OperationDto>> Handle(OperationUpdateEvent request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get {TEventType}: {TEvent}", typeof(OperationUpdateEvent).Name, request);
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null)
            return Result.NotFound(request.Id.ToString());

        if (entity.Guid != request.Guid)
            return Result.Conflict("Operation with same ID has another GUID");

        _logger.LogInformation("Update entity {TEntityType}: {TEntity}", typeof(Operation).Name, entity.ToString());
        var updated_entity = await UpdateAsync(request, entity,cancellationToken);

        if (!updated_entity.IsSuccess)
            return updated_entity.Map<Operation, OperationDto>((at) => throw new ApplicationException("Unexpected result mapping for ok result"));

        await repository.SaveChangesAsync();

        var result = await ToResult(updated_entity, cancellationToken);
        _logger.LogInformation("Update return resut {OperationDtoType}: {TResut}", typeof(OperationDto).Name, result);
        return Result.Success(result);
    }

    protected virtual async Task<Result<Operation>> UpdateAsync(OperationUpdateEvent source, Operation entity, CancellationToken cancellationToken){
        var updated_entity = source.Adapt(entity);
        var source_result = await new Operation.Validator(accountRep, assetRep, categoryRep, budgetRep, operationRep)
                                .ValidateAsync(updated_entity, cancellationToken);
                    
        if (!source_result.IsValid)
            return Result.Invalid(source_result.AsErrors());

        return updated_entity;
    }
    
    protected virtual Task<OperationDto> ToResult(Operation entity, CancellationToken cancellationToken)
    {
        return Task.FromResult(entity.Adapt<OperationDto>());
    }
}