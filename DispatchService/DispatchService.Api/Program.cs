using DispatchService.Api.DTO;
using DispatchService.Api.Services;
using DispatchService.Model.Context;
using DispatchService.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Route = DispatchService.Model.Entities.Route;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DispatchServiceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(
        AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
});

builder.Services.AddScoped<IEntityService<Driver, DriverCreateDto, DriverUpdateDto>, DriversService>();
builder.Services.AddScoped<IEntityService<Transport, TransportCreateDto, TransportUpdateDto>, TransportsService>();
builder.Services.AddScoped<IEntityService<Route, RouteCreateDto, RouteUpdateDto>, RoutesService>();
builder.Services.AddScoped<QueryService>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();