using System.Text.Json.Serialization;
using Wonderfood.ExternalServices;
using Wonderfood.Repository;
using Wonderfood.Service;
using Wonderfood.Worker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.Configure<ExternalServicesSettings>(builder.Configuration.GetSection("ExternalServicesSettings"));
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddServiceLayer();
builder.Services.AddExternalServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

app.UseSwaggerMiddleware();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();