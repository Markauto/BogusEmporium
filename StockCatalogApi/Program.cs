using ServiceStack.Redis;
using StockCatalogApi.Repositories.Interfaces;
using StockCatalogApi.Repositories;

var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false)
        .AddCommandLine(args)
        .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var redisWriteHosts = config.GetSection("Redis:WriteHosts").Get<string[]>();
var redisReadHosts = config.GetSection("Redis:ReadHosts").Get<string[]>();
builder.Services.AddSingleton<IRedisClientsManagerAsync>(
    cm => new PooledRedisClientManager(redisWriteHosts, redisReadHosts) {
        ConnectTimeout = 100
    });
builder.Services.AddSingleton<IStockItemRepositoryAsync, StockItemRepositoryAsync>();
    
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
