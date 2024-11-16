using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Postech.Fase3.Contatos.Add.Infra.Repository.Context;
using Testcontainers.MsSql;

namespace Postech.Fase3.Contatos.Add.Integracao.Test.Fixture;

[CollectionDefinition(nameof(ContextDbCollection))]
public class ContextDbCollection : ICollectionFixture<ContextDbFixture>;

public class ContextDbFixture : IAsyncLifetime
{

    public AppDBContext? Context { get; private set; }
    public string sqlConection { get; private set; } = "";
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPortBinding(1434, true) // Changed port to 1434
        .Build();
    public async Task InitializeAsync()
    {

        await _msSqlContainer.StartAsync();
        sqlConection = _msSqlContainer.GetConnectionString();
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseSqlServer(sqlConection)
            .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning))
            .Options;

        Context = new AppDBContext(options);
        await Context.Database.MigrateAsync();


    }
 
    public async Task DisposeAsync()
    {
        await _msSqlContainer.StopAsync();
    }



}

