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
        // Ensure directory exists
        var dbPath = "/app/datadb";
        if (!Directory.Exists(dbPath))
        {
            Directory.CreateDirectory(dbPath);
            Console.WriteLine($"Created directory: {dbPath}");
        }
        
        var connectionString = app.Configuration.GetConnectionString("DiscountDb");
        Console.WriteLine($"Using connection string: {connectionString}");
        
        // Print current working directory
        Console.WriteLine($"Current working directory: {Directory.GetCurrentDirectory()}");
        
        // Try to create test files in multiple locations
        try {
            File.WriteAllText("/app/test.txt", "test");
            Console.WriteLine("Successfully wrote to /app/test.txt");
        } catch (Exception ex) {
            Console.WriteLine($"Failed to write to /app/test.txt: {ex.Message}");
        }
        
        try {
            File.WriteAllText("/app/Data/test.txt", "test");
            Console.WriteLine("Successfully wrote to /app/Data/test.txt");
        } catch (Exception ex) {
            Console.WriteLine($"Failed to write to /app/Data/test.txt: {ex.Message}");
        }
        
        
        
        // Create Data directory if it doesn't exist
        var dataDir = Path.GetDirectoryName(connectionString?.Replace("Data Source=", "").Split(';')[0]);
        Console.WriteLine($"Data directory: {dataDir}");
        if (!string.IsNullOrEmpty(dataDir) && !Directory.Exists(dataDir))
        {
            Directory.CreateDirectory(dataDir);
            Console.WriteLine($"Created directory: {dataDir}");
        }
        
        // Before running migrations
        Console.WriteLine("Waiting for filesystem initialization...");
        await Task.Delay(5000); // 2-second delay
        Console.WriteLine("Continuing with database migration");

        
        // Configure the HTTP request pipeline.
        // await app.UseMigration();
        await db.Database.MigrateAsync();

        Console.WriteLine("Database created and migrations applied successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration error: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");    }
}


// app.UseHttpsRedirection();

app.MapGrpcService<DiscountService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();