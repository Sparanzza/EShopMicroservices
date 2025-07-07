using BuildingBlocks.Exceptions.Handler;
using CatalogAPI.Data;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    // Register all handlers in the current assembly
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMarten(opts =>
{
    // Configure Marten to use the local PostgreSQL database
    opts.Connection(builder.Configuration.GetConnectionString("CatalogConnection") ?? string.Empty);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    // Enable the Marten Studio in development mode
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();

//Configure the HTTP request pipeline.
app.MapCarter();

// app.UseExceptionHandler(exceptionHandler =>
// {
//     exceptionHandler.Run(async context =>
//     {
//         var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//         if (exception == null)
//         {
//             return;
//         }
//         
//         var problemDetails = new
//         {
//             Title = exception.Message,
//             Status = StatusCodes.Status500InternalServerError,
//             Details = exception.StackTrace
//         };
//         
//         context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//         context.Response.ContentType = "application/problem+json";
//         await context.Response.WriteAsJsonAsync(problemDetails);
//     });
// });

app.UseExceptionHandler(options => { });

app.Run();