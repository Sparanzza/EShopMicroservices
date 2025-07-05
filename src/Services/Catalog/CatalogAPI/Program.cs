using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    // Register all handlers in the current assembly
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(opts =>
{
    // Configure Marten to use the local PostgreSQL database
    opts.Connection(builder.Configuration.GetConnectionString("CatalogConnection"));
}).UseLightweightSessions();

var app = builder.Build();

//Configure the HTTP request pipeline.
app.MapCarter();

app.Run();