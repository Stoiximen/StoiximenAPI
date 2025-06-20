using Stoiximen.Application.Interfaces;
using Stoiximen.Application.Services;
using Stoiximen.Domain.Repositories;
using Stoiximen.Infrastructure.Interfaces;
using Stoiximen.Infrastructure.Repositories;
using Stoiximen.Infrastructure.Services;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();

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
        //services.AddAuthentication(options =>
        //{
        //    options.defaultauthenticatescheme = "telegram";
        //    options.defaultchallengescheme = "telegram";
        //});
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
        //app.UseAuthentication();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
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