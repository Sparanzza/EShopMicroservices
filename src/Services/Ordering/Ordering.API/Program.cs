using Ordering.API;
using Ordering.Application;
using Ordering.infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

// Infrastructure - EF CORE
// Application - CQRS MediatR
// API - Carter Health Checks, etc ...

var app = builder.Build();
app.UseApiServices();
// Add request pipeline.

app.Run();