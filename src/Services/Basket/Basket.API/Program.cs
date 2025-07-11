using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add services to the container.

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});


builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("BasketConnection") ?? string.Empty)
    .AddRedis(builder.Configuration.GetConnectionString("Redis") ?? string.Empty);

// Data Services
builder.Services.AddMarten(opts =>
{
    // Configure Marten to use the local PostgreSQL database
    opts.Connection(builder.Configuration.GetConnectionString("BasketConnection") ?? string.Empty);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

// Grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"] ?? throw new InvalidOperationException())
);

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

// builder.Services.AddScoped<IBasketRepository>(provider =>
// {
//     var basketRepository = provider.GetRequiredService<BasketRepository>();
//     return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
// });


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapCarter();
app.UseHttpsRedirection();

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.UseExceptionHandler(options => { });

app.Run();