using Wonderfood.Repository.Extensions;
using Wonderfood.Repository.Settings;
using Wonderfood.Service.ServiceExtensions;
using Wonderfood.Worker.QueueServices;
using Wonderfood.Worker.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<AzureServiceBusSettings>(builder.Configuration.GetSection("AzureServiceBusSettings"));
builder.Services.AddMongoDbServices(builder.Configuration);
builder.Services.AddServiceLayerExtensions();
builder.Services.AddAzureServiceBusConsumers(builder.Configuration);
builder.Services.AddAzureServiceBusPublisher(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
