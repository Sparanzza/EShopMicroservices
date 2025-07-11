using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddDbContext<DiscountContext>(
    options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("DiscountDb") ?? string.Empty);
    });


var app = builder.Build();

// Create database and run migrations synchronously
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DiscountContext>();
    
    try
    {
        
        // await app.UseMigration();
        await db.Database.MigrateAsync();

        Console.WriteLine("Database created and migrations applied successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration error: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");    }
}


app.UseHttpsRedirection();

app.MapGrpcService<DiscountService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();