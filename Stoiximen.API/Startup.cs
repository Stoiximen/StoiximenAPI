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

        //services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = "Telegram";
        //    options.DefaultChallengeScheme = "Telegram";
        //});

        //services.AddAuthorization(options =>
        //{
        //    options.AddPolicy("TelegramSignedIn", policy =>
        //        policy.RequireClaim("TelegramId"));
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

        //app.UseAuthentication();
        //app.UseAuthorization();

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