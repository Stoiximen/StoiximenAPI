using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stoiximen.API.Middleware;
using Stoiximen.Application.Interfaces;
using Stoiximen.Application.Services;
using Stoiximen.Domain.Repositories;
using Stoiximen.Infrastructure.EF.Context;
using Stoiximen.Infrastructure.Interfaces;
using Stoiximen.Infrastructure.Repositories;
using Stoiximen.Infrastructure.Services;
using System.Text;
using System.Threading.RateLimiting;

public class Startup
{
    private IStoiximenConfiguration _config { get; }

    public Startup(IConfiguration configuration)
    {
        _config = new StoiximenConfiguration(configuration);
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();
        
        services.AddHttpClient("TelegramBotClient", client =>
        {
        });

        //services.AddHttpClient("TelegramBotClient", client =>
        //{
        //    client.BaseAddress = new Uri("https://api.telegram.org/");
        //});

        //.Net standards
        ConfigureSwagger(services);
        ConfigureRateLimiting(services);
        ConfigureAuthentication(services);
        ConfigureDatabase(services);
        ConfigureCors(services);

        RegisterInternalServices(services);
        RegisterExternalServices(services);
        RegisterRepositories(services);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<GlobalExceptionMiddleware>();

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseCors("AllowAllOrigins");

        app.UseRateLimiter();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    //Configure authentication
    private void ConfigureAuthentication(IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = CreateTokenValidationParameters();
        });
    }
    

    // Register services
    private void RegisterInternalServices(IServiceCollection services)
    {
        services.AddSingleton<IStoiximenConfiguration, StoiximenConfiguration>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
    }

    private void RegisterExternalServices(IServiceCollection services)
    {
        services.AddScoped<ITelegramService, TelegramService>();
    }

    // Register repositories
    private void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Stoiximen API",
                Version = "v1",
                Description = "API for Stoiximen application with JWT authentication"
            });

            // Add JWT Bearer authentication to Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Enter your JWT token below."
            });

            // Apply JWT security globally to all endpoints
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });

        });
    }

    private void ConfigureDatabase(IServiceCollection services)
    {
        services.AddDbContext<StoiximenDbContext>(options =>
        {
            options.UseSqlServer(_config.DbConnectionString);
        });
    }

    private void ConfigureCors(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
    }

    private TokenValidationParameters CreateTokenValidationParameters()
    {
        var saltedKey = _config.JwtSecretKey + _config.JwtSalt;
        var key = Encoding.UTF8.GetBytes(saltedKey);

        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = _config.JwtIssuer,
            ValidateAudience = true,
            ValidAudience = _config.JwtAudience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }

    private void ConfigureRateLimiting(IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: "global",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = _config.RequestLimit,
                        Window = TimeSpan.FromMinutes(1)
                    }));
        });
    }
}