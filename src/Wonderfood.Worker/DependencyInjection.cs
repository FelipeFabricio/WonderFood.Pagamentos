using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Wonderfood.Worker;

public static class DependencyInjection
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WonderFood.Pagamentos - Worker", Version = "v1" });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }
    
    public static void UseSwaggerMiddleware(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WonderFood.Pagamentos - Worker"));
    }
}