# Development setup (local Postgres via Docker)

This project uses a local Postgres instance for development to avoid external network and IPv6 issues.

Set environment vars
```bash
export DOTNET_CLI_HOME=$PWD/.dotnet_tmp
export NUGET_PACKAGES=$PWD/.nuget_packages
export TMPDIR=$PWD/.dotnet_tmp
mkdir -p $DOTNET_CLI_HOME $NUGET_PACKAGES $TMPDIR
```


```bash
dotnet restore
dotnet build
# create migrations
dotnet ef migrations add InitialCreate
# apply migrations
dotnet ef database update
# run the app
dotnet run
```

Default development connection string is in `appsettings.Development.json`:
```
Host=localhost;Port=5432;Database=bhl;Username=postgres;Password=postgres;Ssl Mode=Disable
```
