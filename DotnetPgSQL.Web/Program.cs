using DotnetPgSQL.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DotnetPgSQL.Application.Modules;
using DotnetPgSQL.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);

// Configure services
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure middleware pipeline
ConfigureMiddleware(app);

app.Run();

return;
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers();

    // Enable XML comments
    services.AddControllers()
        .AddControllersAsServices();

    services.AddEndpointsApiExplorer();

    ConfigureSwagger(services);

    ConfigureDatabase(services, configuration);

    RegisterApplicationServices(services);
}
void ConfigureSwagger(IServiceCollection services)
{
    services.AddSwaggerGen();
}
void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
{
    services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("ProductOrderDbConnection"))
    );
}
void RegisterApplicationServices(IServiceCollection services)
{
    services.AddApplicationDependencies().AddInfrastructureDependencies();
}
void ConfigureMiddleware(WebApplication application)
{
    // Enable middleware to serve generated Swagger as a JSON endpoint and the Swagger UI
    application.UseSwagger();
    application.UseSwaggerUI();
    application.UseHttpsRedirection();
    application.UseRouting();
    application.UseAuthentication();
    application.UseAuthorization();
    application.MapControllers();
}