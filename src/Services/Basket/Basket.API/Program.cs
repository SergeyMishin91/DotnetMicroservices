using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using MassTransit;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("MyRedisCon"));
    configuration.AbortOnConnectFail = false; // Allow retrying connection
    configuration.ConnectTimeout = 5000; // Set connection timeout (milliseconds)
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddOptions<RedisCacheOptions>().Configure<IServiceProvider>((options, serviceProvider) =>
{
    options.ConnectionMultiplexerFactory = () => Task.FromResult(serviceProvider.GetService<IConnectionMultiplexer>())!;
});

builder.Services.AddStackExchangeRedisCache(_ => { });

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    opt => opt.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!));
builder.Services.AddScoped<DiscountGrpcService>();

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((_, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]!);
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
