# Development setup (local Postgres via Docker)

This project uses a local Postgres instance for development to avoid external network and IPv6 issues.

Prerequisites
- Docker or Podman installed and running
- .NET 9 SDK

Start Postgres
```bash
docker-compose up -d
```

Set environment vars (optional but helpful when dotnet creates temp files)
```bash
export DOTNET_CLI_HOME=$PWD/.dotnet_tmp
export NUGET_PACKAGES=$PWD/.nuget_packages
export TMPDIR=$PWD/.dotnet_tmp
mkdir -p $DOTNET_CLI_HOME $NUGET_PACKAGES $TMPDIR
```

Restore, migrate, run
```bash
dotnet restore
dotnet build
# create migrations (only if you haven't created them already)
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

If you prefer to use Supabase directly, revert the connection string and ensure your machine can reach Supabase over IPv4 or IPv6.
