using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infraestrutura.ModuloTarefa;
using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Dominio.ModuloContato;
using EAgenda.Dominio.ModuloDespesa;
using EAgenda.Infraestrutura.Compartilhado;
using EAgenda.Infraestrutura.ModuloCategoria;
using EAgenda.Infraestrutura.ModuloCompromisso;
using EAgenda.Infraestrutura.ModuloContato;
using EAgenda.Infraestrutura.ModuloDespesa;
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
        });

        builder.Services.AddScoped<ContextoDados>((_) => new ContextoDados(true));
        builder.Services.AddScoped<IRepositorioContato, RepositorioContatoEmArquivo>();
        builder.Services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmArquivo>();
        builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmArquivo>();
        builder.Services.AddScoped<IRepositorioDespesa, RepositorioDespesaEmArquivo>();
        builder.Services.AddScoped<IRepositorioTarefa, RepositorioTarefaEmArquivo>();

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
