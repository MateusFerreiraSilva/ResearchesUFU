using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Services;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;
using System.Reflection;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Repositories;
using ResearchesUFU.API.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddOData(options =>
        options
            .Select()
            .Filter()
            .OrderBy()
            .SetMaxTop(Constants.MAX_TOP)
            .Count()
    ).AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
;

builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: Constants.CORS_POLICY_NAME,
            policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }
        );
    }
);


#region Configuring The Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // adding xml comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    // Add support for OData-like REST endpoint with [EnableQuery]
    options.OperationFilter<ODataOperationFilter>();
});
#endregion

#region Add Database
builder.Services.AddDbContext<ResearchesUFUContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString(Constants.DATEBASE_NAME);
        options.UseNpgsql(connectionString);
    }
);
#endregion

#region Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#region Adding Services

#region Services
builder.Services.AddScoped<IResearchService, ResearchService>();
builder.Services.AddScoped<IUserService, UserService>();
#endregion

#region Repositories
builder.Services.AddScoped<IBaseRepository<Research>, ResearchRepository>();
builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();
#endregion

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(Constants.CORS_POLICY_NAME);

#region Applying Migrations
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<ResearchesUFUContext>();

if (context.Database.GetPendingMigrations().Any()) {
    context.Database.Migrate();
}
#endregion

app.Run();
