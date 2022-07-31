using Catalog.API.DataContext;
using Catalog.API.Extension;
using Catalog.API.Model.Entities;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var mongoClient = new MongoClient();
    var database = mongoClient.GetDatabase(builder.Configuration.GetValue<string>("CatalogDataBaseSettings:DatabaseName"));
    var products = database.GetCollection<Product>(builder.Configuration.GetValue<string>("CatalogDataBaseSettings:CollectionName"));

    // this will simply create payment collection and seed data.
    //var payment = database.GetCollection<Product>("Payment")

    CatalogContextSeed.SeedData(products);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

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
