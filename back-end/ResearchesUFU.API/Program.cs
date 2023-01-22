using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Services;
using ResearchesUFU.API.Services.Interfaces;
using System.Reflection;

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
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Add Db
builder.Services.AddDbContext<ResearchesUFUContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString(Constants.DATEBASE_NAME);
        options.UseNpgsql(connectionString);
    }
);

// Adding Services
builder.Services.AddScoped<IResearchService, ResearchService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
