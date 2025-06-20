using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Stoiximen.Application.Interfaces;
using Stoiximen.Application.Services;
using Stoiximen.Domain.Repositories;
using Stoiximen.Infrastructure.Interfaces;
using Stoiximen.Infrastructure.Repositories;
using Stoiximen.Infrastructure.Services;
using System.Text;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private  IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();

        ConfigureAuthentication(services);
        RegisterInternalServices(services);
        RegisterRepositories(services);

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

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        
        app.UseCors("AllowAllOrigins");

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

    private TokenValidationParameters CreateTokenValidationParameters()
    {
        var config = new StoiximenConfiguration(Configuration);

        var saltedKey = config.JwtSecretKey + config.JwtSalt;
        var key = Encoding.UTF8.GetBytes(saltedKey);

        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = config.JwtIssuer,
            ValidateAudience = true,
            ValidAudience = config.JwtAudience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }

    // Register services
    public void RegisterInternalServices(IServiceCollection services)
    {
        services.AddSingleton<IStoiximenConfiguration, StoiximenConfiguration>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<IAuthService, AuthService>();
    }

    // Register repositories
    public void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
    }
}