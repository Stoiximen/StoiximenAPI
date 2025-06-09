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

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Stoiximen.Application")));

        RegisterInternalServices(services);
        RegisterRepositories(services);

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", //TODO: GN
                policy => policy.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader());
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
        app.UseCors("AllowAll");//TODO: GN
        app.UseAuthorization();//TODO: GN

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    // Register services
    public void RegisterInternalServices(IServiceCollection services)
    {
        services.AddScoped<ISubscriptionService, SubscriptionService>();
    }

    // Register repositories
    public void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
    }
}