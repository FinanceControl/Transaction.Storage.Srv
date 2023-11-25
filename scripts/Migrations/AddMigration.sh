read -p 'Enter Migration name: ' migname
project='../../src/Configurations/DataBase/Transaction.Storage.Srv.Configurations.DataBase/Transaction.Storage.Srv.Configurations.DataBase.csproj'
mig_pr='../../src/Configurations/DataBase/Transaction.Storage.Srv.Configurations.DataBase.Migrations.Factory/Transaction.Storage.Srv.Configurations.DataBase.Migrations.Factory.csproj'
dotnet ef migrations add $migname -p $project -s $mig_pr
