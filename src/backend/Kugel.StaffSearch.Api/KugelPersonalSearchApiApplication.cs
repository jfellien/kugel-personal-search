using System.Reflection;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Kugel.StaffSearch.Database.SqlServer;
using Kugel.StaffSearch.Database.SqlServer.Repositories;
using Kugel.StaffSearch.Api.FluentWebAppBuilder;
using Kugel.StaffSearch.Api.Services;
using Kugel.StaffSearch.Database.Repositories;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace Kugel.StaffSearch.Api;

internal class KugelPersonalSearchApiApplication : IConfigureWebApplication, IWebApplicationRuntime
{
    private readonly WebApplicationBuilder _internalWebApplicationBuilder;
    private bool _applicationIsInFaultedState;
    private WebApplication? _internalWebApplication;

    public static IConfigureWebApplication Configure(string[] args)
    {
        return new KugelPersonalSearchApiApplication(args);
    }

    private KugelPersonalSearchApiApplication(string[] args)
    {
        _internalWebApplicationBuilder = WebApplication.CreateBuilder(args);
    }

    public IConfigureWebApplication SetForwardHeader()
    {
        _internalWebApplicationBuilder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor
                | ForwardedHeaders.XForwardedProto
                | ForwardedHeaders.XForwardedHost;

            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        return this;
    }

    public IConfigureWebApplication SetCors()
    {
        if (AppIsInDevelopment())
        {
            _internalWebApplicationBuilder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            }));
        }

        return this;
    }

    public IConfigureWebApplication SetAuthorization()
    {
        _internalWebApplicationBuilder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(_internalWebApplicationBuilder.Configuration, "AzureAdB2C");

        _internalWebApplicationBuilder.Services.AddAuthorization();

        return this;
    }

    public IConfigureWebApplication SetApplicationLogging()
    {
        _internalWebApplicationBuilder.Services.AddLogging(options =>
        {
            options.AddConsole();
            options.SetMinimumLevel(LogLevel.Trace);

            options.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Trace);

            options.AddApplicationInsights(
                telemetryConfiguration =>
                {
                    telemetryConfiguration.DefaultTelemetrySink.TelemetryProcessorChainBuilder
                        .Use(next => new QuickPulseTelemetryProcessor(next)).Build();
                },
                loggerOptions => { loggerOptions.TrackExceptionsAsExceptionTelemetry = true; });
        });

        return this;
    }

    public IConfigureWebApplication SetApplicationTelemetryLogging()
    {
        _internalWebApplicationBuilder.Services.AddApplicationInsightsTelemetry();

        return this;
    }

    public IConfigureWebApplication SetApplicationServices()
    {
        _internalWebApplicationBuilder.Services.AddScoped<IStaffMemberRepository, StaffMemberRepository>();
        _internalWebApplicationBuilder.Services.AddScoped<IPersonService, PersonService>();

        return this;
    }

    public IConfigureWebApplication SetDatabase()
    {
        _internalWebApplicationBuilder.Services.AddDbContext<KugelPersonalSearchContext>(options =>
        {
            string? connectionString = AppIsInDeployment()
                ? "no connection needed"
                : _internalWebApplicationBuilder.Configuration["SqlServerConnection"];
            
            options.UseSqlServer(
                connectionString,
                b => b.MigrationsAssembly("Kugel.StaffSearch.Database.SqlServer.Migrations"));
        });

        return this;
    }

    public IConfigureWebApplication SetHttpClients()
    {
        // No Http Clients needed

        return this;
    }

    public IConfigureWebApplication SetControllers()
    {
        _internalWebApplicationBuilder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(x => x.SuppressMapClientErrors = true);

        return this;
    }

    public IConfigureWebApplication SetOpenApi()
    {
        _internalWebApplicationBuilder.Services.AddEndpointsApiExplorer();
        _internalWebApplicationBuilder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Kugel Personal Search API",
                    Version = "v1"
                }
            );

            // If this API is running in Production,
            // we need an additional Path element in the OpenAPI Routes
            if (AppIsInProduction())
            {
                options.AddServer(new OpenApiServer
                {
                    Url = _internalWebApplicationBuilder.Configuration["APIPathBase"]
                });
            }

            string filePath = Path.Combine(
                AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

            options.IncludeXmlComments(filePath);

            options.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter into field the Bearer token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

        return this;
    }

    public IWebApplicationRuntime ApplyConfigurations()
    {
        _internalWebApplication = _internalWebApplicationBuilder.Build();

        return this;
    }

    public IWebApplicationRuntime EnableForwardHeaders()
    {
        _internalWebApplication?.UseForwardedHeaders();

        return this;
    }

    public IWebApplicationRuntime EnableExceptionHandler()
    {
        _internalWebApplication?.UseExceptionHandler(ExceptionHandler.HandleException);

        return this;
    }

    public IWebApplicationRuntime EnableCors()
    {
        if (AppIsInDevelopment())
        {
            _internalWebApplication?.UseCors();
        }

        return this;
    }

    public IWebApplicationRuntime EnableAuthentication()
    {
        _internalWebApplication?.UseAuthentication();

        return this;
    }

    public IWebApplicationRuntime EnableAuthorization()
    {
        _internalWebApplication?.UseAuthorization();

        return this;
    }

    public IWebApplicationRuntime EnableController()
    {
        _internalWebApplication?.MapControllers();

        return this;
    }

    public IWebApplicationRuntime EnableOpenApi()
    {
        _internalWebApplication.UseSwagger();
        _internalWebApplication.UseSwaggerUI(c =>
        {
            c.RoutePrefix = "swagger";
            c.SwaggerEndpoint("v1/swagger.json", "Kugel Personal Search Api V1");
        });

        return this;
    }

    public IWebApplicationRuntime EnableDatabase()
    {
        using (KugelPersonalSearchContext? dbContext = _internalWebApplication?.Services.CreateScope().ServiceProvider
                   .GetRequiredService<KugelPersonalSearchContext>())
        {
            try
            {
                dbContext?.Database.EnsureCreated();
            }
            catch (SqlException ex)
            {
                string errorMessage = $"Unable to start the Application due to an SQL Server error: {ex.Message}";

                _internalWebApplication?.Logger.LogCritical(errorMessage);


                ConsoleColor defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errorMessage);
                Console.ForegroundColor = defaultColor;

                _applicationIsInFaultedState = true;
            }
        }

        return this;
    }

    public void RunApplication()
    {
        if (_applicationIsInFaultedState == false)
        {
            _internalWebApplication?.Run();
        }

        if (AppIsInDevelopment() == false) return;

        Console.WriteLine("Close the app with any key...");
        Console.ReadLine();
    }

    private bool AppIsInDevelopment()
    {
        return _internalWebApplicationBuilder.Environment.IsDevelopment();
    }

    private bool AppIsInProduction()
    {
        return _internalWebApplicationBuilder.Environment.IsProduction();
    }

    private bool AppIsInDeployment()
    {
        Console.WriteLine($"Environment: {_internalWebApplicationBuilder.Environment.EnvironmentName}");
        
        return _internalWebApplicationBuilder.Environment.EnvironmentName == "Deployment";
    }
}