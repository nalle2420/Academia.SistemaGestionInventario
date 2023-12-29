using Academia.SistemaGestionInventario.WApi._Features.Permisos;
using Academia.SistemaGestionInventario.WApi._Features.Productos;
using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventarioDetalle;
using Academia.SistemaGestionInventario.WApi._Features.Sucursales;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios;
using Academia.SistemaGestionInventario.WApi.Domain.General;
using Academia.SistemaGestionInventario.WApi.Domain.ProductoLote;
using Academia.SistemaGestionInventario.WApi.Domain.SalidaInvenario;
using Academia.SistemaGestionInventario.WApi.Domain.UsuarioD;
using Academia.SistemaGestionInventario.WApi.Infrastructure;
using Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario;
using AutoMapper;
using Farsiman.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile(new MapProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var connectionString = builder.Configuration.GetConnectionString("EFCoreGestionInventario");
builder.Services.AddDbContext<GestionInventarioDbContext>(opciones => opciones.UseSqlServer(connectionString));


builder.Services.AddTransient<UnitOfWorkBuilder, UnitOfWorkBuilder>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerForFsIdentityServer(opt =>
{
    opt.Title = "Proyecto Gestion de inventario";
    opt.Description = "Proyecto para gestionar las salidas de inventario";
    opt.Version = "v1.0";
});

builder.Services.AddFsAuthService(configureOptions =>
{
    configureOptions.Username = builder.Configuration.GetFromENV("Configurations:FsIdentityServer:Username");
    configureOptions.Password = builder.Configuration.GetFromENV("Configurations:FsIdentityServer:Password");
});

builder.Services.AddTransient<SucursalService>();
builder.Services.AddTransient<GeneralDomain>();
builder.Services.AddTransient<ProductoLoteService>();
builder.Services.AddTransient<UsuarioService>();
builder.Services.AddTransient<ProductoService>();
builder.Services.AddTransient<SalidaInventarioService>();
builder.Services.AddTransient<SalidaInventarioDetalleService>();
builder.Services.AddTransient<ProductoLoteDomain>();
builder.Services.AddTransient<SalidaInventarioDomain>();
builder.Services.AddTransient<UsuarioDomain>();
builder.Services.AddTransient<PermisoService>();








builder.Services.AddTransient<ProductoLoteDomain>();





var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithFsIdentityServer();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
