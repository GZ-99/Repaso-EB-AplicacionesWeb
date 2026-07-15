using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using LetPot.Platform.u202416903.Allocation.Application.CommandServices;
using LetPot.Platform.u202416903.Allocation.Application.Internal.CommandServices;
using LetPot.Platform.u202416903.Allocation.Application.Internal.EventHandlers;
using LetPot.Platform.u202416903.Allocation.Domain.Model.Events;
using LetPot.Platform.u202416903.Allocation.Application.Internal.QueryServices;
using LetPot.Platform.u202416903.Allocation.Application.QueryServices;
using LetPot.Platform.u202416903.Allocation.Domain.Repositories;
using LetPot.Platform.u202416903.Allocation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using LetPot.Platform.u202416903.Shared.Domain.Model.Events;
using LetPot.Platform.u202416903.Shared.Domain.Repositories;
using LetPot.Platform.u202416903.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using LetPot.Platform.u202416903.Shared.Infrastructure.Mediator.Cortex.Configuration;
using LetPot.Platform.u202416903.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using LetPot.Platform.u202416903.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using LetPot.Platform.u202416903.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using LetPot.Platform.u202416903.Shared.Interfaces.Rest.ProblemDetails;
using LetPot.Platform.u202416903.Shared.Resources.CommonMessages;
using LetPot.Platform.u202416903.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

builder.Services.AddControllers();

// Add ProblemDetails services
builder.Services.AddProblemDetails();

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add Database Connection

// Configure Database Context and route EF logs through the app logger pipeline.
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Explicitly register IStringLocalizer for ErrorMessages and Commons
builder.Services.AddSingleton<IStringLocalizer<ErrorMessages>, StringLocalizer<ErrorMessages>>();
builder.Services
    .AddSingleton<IStringLocalizer<CommonMessages>,
        StringLocalizer<CommonMessages>>();

// Register the custom ProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "LetPot.Platform.u202416903", //Esto es nuevo
            Version = "v1",
            Description = "LetPot Platform API", //Esto es nuevo
            TermsOfService = new Uri("https://letpot.com/"), //Esto es nuevo
            Contact = new OpenApiContact
            {
                Name = "LetPot", //Esto es nuevo
                Email = "contact@letpot.com" //Esto es nuevo
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        { [new OpenApiSecuritySchemeReference("bearer", document)] = [] });
    options.EnableAnnotations();
});

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Allocation Bounded Context
builder.Services.AddScoped<IPotRepository, PotRepository>(); //Esto es nuevo
builder.Services.AddScoped<IPotQueryService, PotQueryService>(); //Esto es nuevo
builder.Services.AddScoped<IPotCommandService, PotCommandService>(); //Esto es nuevo
builder.Services.AddScoped<ApplicationReadyEventHandler>(); //Esto es nuevo

// TokenSettings Configuration

//builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

// Mediator Configuration

// Add Mediator Injection Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    [typeof(Program)]);

var app = builder.Build();

// Apply pending migrations on startup (safe to call even when schema is up to date)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();

    var eventHandler = services.GetRequiredService<ApplicationReadyEventHandler>(); //Esto es nuevo
    await eventHandler.Handle(new ApplicationReadyEvent(), CancellationToken.None); //Esto es nuevo
}

// Configure the HTTP request pipeline.
app.UseGlobalExceptionHandler();

var supportedCultures = new[] { "en", "es" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

// Add Authorization Middleware to Pipeline
// app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
