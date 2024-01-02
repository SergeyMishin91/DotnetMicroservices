using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddScoped<OrderContextSeed>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

SeedDatabase();

app.UseAuthorization();

app.MapControllers();

app.Run();


void SeedDatabase()
{
    using var scope = app.Services.CreateScope();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<OrderContextSeed>>();
    var orderContext = scope.ServiceProvider.GetRequiredService<OrderContext>();

    OrderContextSeed.SeedDataAsync(orderContext, logger).Wait();
}