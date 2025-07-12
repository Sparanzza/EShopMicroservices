var builder = WebApplication.CreateBuilder(args);


//Add services to the container.

var app = builder.Build();

// Add request pipeline.

app.Run();