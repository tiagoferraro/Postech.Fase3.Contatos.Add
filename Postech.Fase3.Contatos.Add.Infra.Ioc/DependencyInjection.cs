using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postech.Fase3.Contatos.Add.Application.Interface;
using Postech.Fase3.Contatos.Add.Application.Service;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting;
using Postech.Fase3.Contatos.Add.Infra.Interface;
using Postech.Fase3.Contatos.Add.Infra.Messaging;
using Postech.Fase3.Contatos.Add.Infra.Repository;
using Postech.Fase3.Contatos.Add.Infra.Repository.Context;

namespace Postech.Fase3.Contatos.Add.Infra.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AdicionarDbContext(this IServiceCollection services,IConfiguration configurarion
    )
    {
        services.AddDbContext<AppDBContext>(options =>
        {
            options.UseSqlServer(configurarion.GetConnectionString("DefaultConnection"));
        }); 
       
       
        return services;
    }
    public static IServiceCollection AdicionarDependencias(this IServiceCollection services)
    {
        services.AddSingleton<RabbitMqConsumer>();
        services.AddScoped<IMessageProcessor, MensagemService>();
        
        services.AddScoped<IContatoRepository, ContatoRepository>();
        services.AddScoped<IContatoService, ContatoService>();

        return services;
    }

}