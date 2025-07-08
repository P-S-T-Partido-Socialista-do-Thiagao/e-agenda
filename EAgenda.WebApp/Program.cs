using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infraestrutura.SqlServer.ModuloCategoria;
using eAgenda.Infraestrutura.SqlServer.ModuloCompromisso;
using eAgenda.Infraestrutura.SqlServer.ModuloContato;
using eAgenda.Infraestrutura.SqlServer.ModuloDespesa;
using eAgenda.Infraestrutura.SqlServer.ModuloTarefa;
using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Dominio.ModuloContato;
using EAgenda.Dominio.ModuloDespesa;
using EAgenda.Infraestrutura.Compartilhado;
using EAgenda.WebApp.ActionFilters;
using EAgenda.WebApp.DependencyInjection;
using Serilog;

namespace EAgenda.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add<ValidarModeloAttribute>();
            options.Filters.Add<LogarAcaoAttribute>();
        });

        builder.Services.AddScoped<ContextoDados>((_) => new ContextoDados(true));
        builder.Services.AddScoped<IRepositorioContato, RepositorioContatoEmSql>(); 
        builder.Services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmSql>();
        builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmSql>();
        builder.Services.AddScoped<IRepositorioDespesa, RepositorioDespesaEmSql>();
        builder.Services.AddScoped<IRepositorioTarefa, RepositorioTarefaEmSql>();

        builder.Services.AddSerilogConfig(builder.Logging);

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
            app.UseExceptionHandler("/erro");
        else
            app.UseDeveloperExceptionPage();

        app.UseAntiforgery();
        app.UseStaticFiles();
        app.UseRouting();
        app.MapDefaultControllerRoute();

        app.Run();

    }
}
