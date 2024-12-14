using Ardalis.Specification;

namespace Transaction.Storage.Srv.Shared.Database.Specification;
public abstract class UIXSpecificationFactory<TEntity,TModel> where TEntity : class
{
    public UIXSpecificationFactory(IEnumerable<string> col_name_arr, bool isUnique)
    {
        ColumnNames = col_name_arr;
        IsUnique = isUnique;
    }
    public abstract Specification<TEntity> Build(TModel model);
    public IEnumerable<string> ColumnNames { get; }
    public bool IsUnique { get; }
}