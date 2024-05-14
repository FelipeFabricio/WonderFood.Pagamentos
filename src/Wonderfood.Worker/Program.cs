using System.Text.Json.Serialization;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/_health", () => Results.Ok("Healthy"));
    endpoints.MapHealthChecks("/_ready", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        ResultStatusCodes =
        {
            [HealthStatus.Healthy] = StatusCodes.Status200OK,
            [HealthStatus.Degraded] = StatusCodes.Status200OK,
            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
        }
    });
});
app.Run();