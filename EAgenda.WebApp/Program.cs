using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infraestrutura.Orm.Compartilhado;
using eAgenda.Infraestrutura.Orm.ModuloContato;
using eAgenda.Infraestrutura.SqlServer.ModuloCategoria;
using eAgenda.Infraestrutura.SqlServer.ModuloCompromisso;
using eAgenda.Infraestrutura.SqlServer.ModuloContato;
using eAgenda.Infraestrutura.SqlServer.ModuloDespesa;
using eAgenda.Infraestrutura.SqlServer.ModuloTarefa;
using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Dominio.ModuloContato;
using EAgenda.WebApp.ActionFilters;
using EAgenda.WebApp.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

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

        // REGISTRA O DbContext
        builder.Services.AddDbContext<eAgendaDbContext>(options =>
        {
            options.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=eAgendaDb;Integrated Security=True"
            );
        });

        // REGISTRA IDbConnection para os reposit�rios que usam SQL
        builder.Services.AddScoped<IDbConnection>(sp =>
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=eAgendaDb;Integrated Security=True";
            return new SqlConnection(connectionString);
        });

        // REGISTRA OS REPOSIT�RIOS
        builder.Services.AddScoped<IRepositorioContato, RepositorioContatoEmOrm>();
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
