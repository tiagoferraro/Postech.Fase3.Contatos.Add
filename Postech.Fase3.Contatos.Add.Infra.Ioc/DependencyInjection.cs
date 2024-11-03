using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postech.Fase3.Contatos.Add.Application.Service;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting;
using Postech.Fase3.Contatos.Add.Infra.Ioc.Messaging;

namespace Postech.Fase3.Contatos.Add.Infra.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AdicionarDBContext(this IServiceCollection services,IConfiguration configurarion
    )
    {
       /* services.AddDbContext<AppDBContext>(options =>
        {
            options.UseSqlServer(configurarion.GetConnectionString("DefaultConnection"));
        }); */
       
       
        return services;
    }
    public static IServiceCollection AdicionarDependencias(this IServiceCollection services)
    {
        services.AddSingleton<RabbitMqConsumer>();
        services.AddSingleton<IMessageProcessor, ContatoService>();
        


        return services;
    }

}