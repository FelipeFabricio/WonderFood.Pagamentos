using System.Text.Json.Serialization;
using MassTransit;
using WonderFood.Models.Events;
using Wonderfood.Repository;
using Wonderfood.Service;
using Wonderfood.Worker;
using Wonderfood.Worker.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddServiceLayer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var rabbitMqUser = builder.Configuration["RABBITMQ_DEFAULT_USER"];
var rabbitMqPassword = builder.Configuration["RABBITMQ_DEFAULT_PASS"];
var rabbitMqHost = builder.Configuration["RABBITMQ_HOST"];
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddConsumer<PagamentoSolicitadoConsumer>();
    busConfigurator.AddConsumer<ReembolsoSolicitadoConsumer>();
    
    busConfigurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqHost, hst =>
        {
            hst.Username(rabbitMqUser);
            hst.Password(rabbitMqPassword);
        });
        
        cfg.ReceiveEndpoint("pagamento_solicitado", e =>
        {
            e.ConfigureConsumer<PagamentoSolicitadoConsumer>(context);
            e.Bind("WonderFood.Models.Events:PagamentoSolicitadoEvent", x =>
            {
                x.RoutingKey = "pagamento.solicitado";
                x.ExchangeType = "fanout";
            });
        });
        
        cfg.ReceiveEndpoint("reembolso_solicitado", e =>
        {
            e.ConfigureConsumer<ReembolsoSolicitadoConsumer>(context);
            e.Bind("WonderFood.Models.Events:ReembolsoSolicitadoEvent", x =>
            {
                x.RoutingKey = "reembolso.solicitado";
                x.ExchangeType = "fanout";
            });
        });
        
        cfg.Publish<PagamentoProcessadoEvent>(x =>
        {
            x.ExchangeType = "fanout";
        });
        
        cfg.Publish<ReembolsoProcessadoEvent>(x =>
        {
            x.ExchangeType = "fanout";
        });
    });
});

var app = builder.Build();

app.UseSwaggerMiddleware();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();