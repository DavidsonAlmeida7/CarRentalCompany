using CarRentalCompany.Data;
using CarRentalCompany.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CarRentalCompany
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Uso de interface: Isso quer dizer que sempre que um ICarRentalCompanyContext for necessário, crie um CarRentalCompanyContext e passe-o.
            builder.Services.AddDbContext<ICarRentalCompanyContext, CarRentalCompanyContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("CarRentalCompanyContext"), new MySqlServerVersion(new Version()), 
                    builder => builder.MigrationsAssembly("CarRentalCompany")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Registra o servico de Seeding no sistema de injeção de dependência da aplicação
            builder.Services.AddScoped<ClienteService>();
            builder.Services.AddScoped<VeiculoService>();
            builder.Services.AddScoped<LocacaoService>();

            var app = builder.Build();

            //Adicionando locale padrão
            var enUS = new CultureInfo("en-US", false);
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS }
            };

            app.UseRequestLocalization(localizationOptions);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}