using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Transaction.Storage.Srv.API.WebApi.Controllers.AccountComponent;
using Transaction.Storage.Srv.API.WebApi.Controllers.AssetComponent;
using Transaction.Storage.Srv.API.WebApi.Controllers.BudgetComponent;
using Transaction.Storage.Srv.API.WebApi.Controllers.CategoryComponent;
using Transaction.Storage.Srv.API.WebApi.Controllers.TransactionComponent;

namespace Transaction.Storage.Srv.API.WebApi;
/// <summary>
/// Swagger doc init tools
/// </summary>
public static class SwaggerGenOptionsInit
{

  /// <summary>
  /// Init Swagger documentations
  /// </summary>
  /// <param name="options"></param>
  public static void Init(this SwaggerGenOptions options)
  {
    options
    .InitAccountApiInfo()
    .InitAssetApiInfo()
    .InitBudgetApiInfo()
    .InitCategoryApiInfo()
    .InitTransactionApiInfo();
  }

  /// <summary>
  /// Init swagger doc api config
  /// </summary>
  /// <param name="options"></param>
  public static void Init(this SwaggerUIOptions options)
  {
    options
    .InitAccountApiInfo()
    .InitAssetApiInfo()
    .InitBudgetApiInfo()
    .InitCategoryApiInfo()
    .InitTransactionApiInfo();
  }
}