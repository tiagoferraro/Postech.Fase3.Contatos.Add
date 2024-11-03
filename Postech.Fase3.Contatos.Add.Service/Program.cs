using Postech.Fase3.Contatos.Add.Infra.Ioc;
using Postech.Fase3.Contatos.Add.Service;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AdicionarDependencias();

var host = builder.Build();
await host.RunAsync();