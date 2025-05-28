using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using SBODeskReact.Infrastrcture.Services;
using webapp.Application;
using webapp.Domain;
using webapp.Infrastrcture;
using webapp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"), b => b.MigrationsAssembly("webapp.Server"));

});
var config = new ConfigurationBuilder()
   .SetBasePath(System.IO.Directory.GetCurrentDirectory())
   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
   .Build();

NLog.LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization using Bearer",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Id="Bearer",
                    Type=ReferenceType.SecurityScheme
                },
                Scheme="oauth2",
                Name="Bearer",
                In=ParameterLocation.Header
            },
            new List<string>()
        }

    });

});


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

builder.Services.AddScoped(typeof(ISAPRepository<>), typeof(SAPRepository<>));

builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

builder.Services.AddScoped<IRepository<SAPDatabases>, Repository<SAPDatabases>>();

builder.Services.AddScoped<IService<SAPDatabases>, SAPDatabaseService>();

builder.Services.AddScoped<ISAPRepository<BusinessPartner>, SAPRepository<BusinessPartner>>();

builder.Services.AddScoped<ISAPService<BusinessPartner>, BusinessPartnerService>();

builder.Services.AddScoped<IServiceLayerService, ServiceLayerService>();



builder.Services.AddScoped<ISLConnectionFactory>(serviceProvider =>
{
    var connectionInfos = new List<webapp.Domain.ConnectionInfo>();
    using (var scope = serviceProvider.CreateScope())
    {
        var databases = scope.ServiceProvider.GetRequiredService<IService<SAPDatabases>>();
        // Synchronously fetching data; consider redesigning if possible
        var databaseList = databases.GetAllAsync().GetAwaiter().GetResult();
        if (databaseList.Count() > 0)
        {
            foreach (var database in databaseList)
            {
                if (database != null)
                {
                    connectionInfos.Add(new webapp.Domain.ConnectionInfo
                    {
                        Name = database?.CompanyDB ?? "",
                        Database = database?.CompanyDB ?? "",
                        Url = database?.ServiceLayerURL ?? "",
                        Username = database?.SAPUsername ?? "",
                        Password = database?.SAPPassword ?? ""
                    });
                }

            }
        }
        else
        {
            connectionInfos.Add(new webapp.Domain.ConnectionInfo
            {
                Name = "Global_Test",
                Database = "Global_Test",
                Url = "https://abit.bot:50000/b1s/v2",
                Username = "manager",
                Password = "P@ssw0rd"
            });

        }

    }
    return new SLConnectionFactory(connectionInfos);
});



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment() || (app.Environment.IsProduction()))
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiCore v1"));
}
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
