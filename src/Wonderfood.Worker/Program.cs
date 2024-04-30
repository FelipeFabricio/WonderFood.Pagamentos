using Wonderfood.Repository;
using Wonderfood.Service;
using Wonderfood.Worker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddServiceLayerExtensions();
builder.Services.AddAzureServiceBus(builder.Configuration);
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
