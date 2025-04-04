using Microsoft.EntityFrameworkCore;
using Reserva.Infraestructure.Data;
using Reserva.Web.Middleware;
using Serilog.Events;
using Serilog;
using System.Text;
using Reserva.Infraestructure.Repository.Interfaces;
using Reserva.Infraestructure.Repository.Implementations;
using Reserva.Application.Services.Interfaces;
using Reserva.Application.Services.Implementations;
using Reserva.Application.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar D.I.
//Repository 
builder.Services.AddTransient<IRepositoryReservacion, RepositoryReservacion>();
builder.Services.AddTransient<IRepositoryHabitacion, RepositoryHabitacion>();
builder.Services.AddTransient<IRepositoryBarco, RepositoryBarco>();
builder.Services.AddTransient<IRepositoryCrucero, RepositoryCrucero>();
builder.Services.AddTransient<IRepositoryPuerto, RepositoryPuerto>();

//Services
builder.Services.AddTransient<IServiceReservacion, ServiceReservacion>();
builder.Services.AddTransient<IServiceHabitacion, ServiceHabitacion>();
builder.Services.AddTransient<IServiceBarco, ServiceBarco>();
builder.Services.AddTransient<IServiceCrucero, ServiceCrucero>();
builder.Services.AddTransient<IServicePuerto, ServicePuerto>();

//Configurar Automapper 
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ReservacionProfile>();
    config.AddProfile<HabitacionProfile>();
    config.AddProfile<BarcoProfile>();
    config.AddProfile<BarcoHabitacionProfile>();
    config.AddProfile<CruceroProfile>();
    config.AddProfile<PuertoProfile>();
});

// Configuar Conexión a la Base de Datos SQL 
builder.Services.AddDbContext<ThousandSunnyContext>(options =>
{
    // it read appsettings.json file 
options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDataBase"));
    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

//Configuración Serilog 
// Logger. P.E. Verbose = muestra SQl Statement 
var logger = new LoggerConfiguration()
// Limitar la información de depuración 
.MinimumLevel.Override("Microsoft", LogEventLevel.Error)
.Enrich.FromLogContext() 
// Log LogEventLevel.Verbose muestra mucha información, pero no es necesaria solo para el proceso de depuración 
.WriteTo.Console(LogEventLevel.Information) 
.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == 
LogEventLevel.Information).WriteTo.File(@"Logs\Info-.log", shared: true, encoding:
Encoding.ASCII, rollingInterval: RollingInterval.Day)) 
.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level ==
LogEventLevel.Debug).WriteTo.File(@"Logs\Debug-.log", shared: true, encoding:
System.Text.Encoding.ASCII, rollingInterval: RollingInterval.Day))
.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level ==
LogEventLevel.Warning).WriteTo.File(@"Logs\Warning-.log", shared: true, encoding:
System.Text.Encoding.ASCII, rollingInterval: RollingInterval.Day))
.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level ==
LogEventLevel.Error).WriteTo.File(@"Logs\Error-.log", shared: true, encoding: Encoding.ASCII,
rollingInterval: RollingInterval.Day))
.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level ==
LogEventLevel.Fatal).WriteTo.File(@"Logs\Fatal-.log", shared: true, encoding: Encoding.ASCII,
rollingInterval: RollingInterval.Day))
.CreateLogger();
builder.Host.UseSerilog(logger);
//*************************** 



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    // Error control Middleware 
    app.UseMiddleware<ErrorHandlingMiddleware>();
}

//Activar soporte a la solicitud de registro con SERILOG 
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Activar Antiforgery  
app.UseAntiforgery();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
