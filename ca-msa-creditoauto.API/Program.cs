using camsacreditoauto.API.Middleware;
using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Infrastructure.Services;
using camsacreditoauto.Repository;
using camsacreditoauto.Repository.Context;
using camsacreditoauto.Repository.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<CreditoAutoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<CreditoAutoContext>(options => options.UseInMemoryDatabase(databaseName: "InMemory_DB"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Registro Interfaces

builder.Services.AddTransient<IClienteInfraestructura, ClienteInfraestructura>();
builder.Services.AddTransient<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddTransient<IPersonaRepositorio, PersonaRepositorio>();
builder.Services.AddTransient<IPatioAutoInfraestructura, PatioAutoInfraestructura>();
builder.Services.AddTransient<IPatioAutoRepositorio, PatioAutoRepositorio>();
builder.Services.AddTransient<IClientePatioInfraestructura,ClientePatioInfraestructura>();
builder.Services.AddTransient<IClientePatioRepositorio, ClientePatioRepositorio>();
builder.Services.AddTransient<ISolicitudInfraestructura, SolicitudInfraestructura>();
builder.Services.AddTransient<ISolicitudRepositorio, SolicitudRepositorio>();
builder.Services.AddTransient<IVehiculoInfraestructura, VehiculoInfraestructura>();
builder.Services.AddTransient<IVehiculoRepositorio, VehiculoRepositorio>();

#endregion

var app = builder.Build();

var scope = app.Services.CreateScope();
var _context = scope.ServiceProvider.GetService<CreditoAutoContext>();
DbInitializer.InitializeDb(_context ?? null!);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();
app.Run();
