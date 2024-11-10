using Postech.Fase3.Contatos.Add.Infra.Ioc;
using Postech.Fase3.Contatos.Add.Service;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<WkAddContato>();
builder.Services.AdicionarDependencias();
builder.Services.AdicionarDBContext(builder.Configuration);

var host = builder.Build();
await host.RunAsync();