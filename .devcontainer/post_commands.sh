dotnet tool install --global dotnet-ef
echo 'export PATH="$PATH:/home/vscode/.dotnet/tools"' >> ~/.zshrc
dotnet dev-certs https
dotnet build /workspaces/Transaction.Storage.Srv/Transaction.Storage.Srv.sln 