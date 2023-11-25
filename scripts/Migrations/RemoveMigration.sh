project='../../src/Configurations/DataBase/Transaction.Storage.Srv.Configurations.DataBase/Transaction.Storage.Srv.Configurations.DataBase.csproj'
mig_pr='../../src/Configurations/DataBase/Transaction.Storage.Srv.Configurations.DataBase/Transaction.Storage.Srv.Configurations.DataBase.Migrations.Factory.csproj'
dotnet ef migrations remove -p $project -s $mig_pr