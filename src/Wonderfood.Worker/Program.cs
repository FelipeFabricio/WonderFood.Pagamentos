using Wonderfood.Core.Interfaces;
using Wonderfood.Database.Context;
using Wonderfood.Database.Repositories;
using Wonderfood.Database.Settings;
using Wonderfood.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<MongoDbContext>();
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();


var host = builder.Build();
host.Run();