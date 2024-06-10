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

// var rabbitMqUser = Configuration["RABBITMQ_DEFAULT_USER"];
// var rabbitMqPassword = Configuration["RABBITMQ_DEFAULT_PASS"];
// var rabbitMqHost = Configuration["RABBITMQ_HOST"];
var rabbitMqUser = "useradmin";
var rabbitMqPassword = "senhaForte123!";
var rabbitMqHost = "amqp://wonderfood_mq:5672";

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddConsumer<PagamentoSolicitadoConsumer>();
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
        
        cfg.Publish<PagamentoProcessadoEvent>(x =>
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