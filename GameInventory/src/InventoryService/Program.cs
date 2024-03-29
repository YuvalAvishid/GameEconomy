using GameCommon.MongoDB;
using GameCommon.MassTransit;
using InventoryService.Entities;
using InventoryService;
using InventoryService.SyncDataClient.Grpc;
using InventoryService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
// Add services to the container.
builder.Services.AddMongo()
                .AddMongoRepository<InventoryItem>("inventoryItems")
                .AddMongoRepository<ProductItem>("productItems")
                .AddMassTransitWithRabbitMq();

builder.Services.AddScoped<IProductDataClient, ProductDataClient>();
                
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

//app.UseHttpsRedirection();

app.MapControllers();

_ = PrepData.PrepPoulation(app);
app.Run();

