using Microsoft.EntityFrameworkCore;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Repository.Context;

using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CreditoAutoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IRepository,PostImplementacion>();
builder.Services.AddScoped<IAsignarClienteInfraestructure, AsignarClienteInfraestructure>();

//services.AddTransient<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
