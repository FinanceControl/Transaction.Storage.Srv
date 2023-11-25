dotnet tool install --global dotnet-ef
echo 'export PATH="$PATH:/home/vscode/.dotnet/tools"' >> ~/.zshrc
dotnet build /workspaces/Transaction.Storage.Srv/Transaction.Storage.Srv.sln 