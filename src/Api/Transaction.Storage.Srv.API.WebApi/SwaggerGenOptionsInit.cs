using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Transaction.Storage.Srv.API.WebApi.Controllers.AccountComponent;
using Transaction.Storage.Srv.API.WebApi.Controllers.AssetComponent;
using Transaction.Storage.Srv.API.WebApi.Controllers.BudgetComponent;
using Transaction.Storage.Srv.API.WebApi.Controllers.CategoryComponent;
using Transaction.Storage.Srv.API.WebApi.Controllers.TransactionComponent;

namespace Transaction.Storage.Srv.API.WebApi;
public static class SwaggerGenOptionsInit
{
  public static void Init(this SwaggerGenOptions options)
  {
    options
    .InitAccountApiInfo()
    .InitAssetApiInfo()
    .InitBudgetApiInfo()
    .InitCategoryApiInfo()
    .InitTransactionApiInfo();
  }

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