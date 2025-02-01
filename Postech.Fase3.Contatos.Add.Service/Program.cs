using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Postech.Fase3.Contatos.Add.Infra.Ioc;
using Postech.Fase3.Contatos.Add.Service;
using Prometheus;
using Serilog;

var builder = Host
    .CreateDefaultBuilder(args)

    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.Configure(app =>
        {
            app.UseRouting();
            app.UseMetricServer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapMetrics(); // Add this line to map the /metrics endpoint
            });
        });

        webBuilder.UseUrls("http://+:8080");
    })
    .ConfigureServices((hostContext, services) =>
    {

        services.AddHostedService<WkAddContato>();
        services.AdicionarDependencias();
        services.AdicionarDbContext(hostContext.Configuration);
    })
    .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console());



await builder.Build().RunAsync();


    
  