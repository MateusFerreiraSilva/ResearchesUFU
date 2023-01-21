using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Db
builder.Services.AddDbContext<ResearchesUFUContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("ResearchesUFU");
        options.UseNpgsql(connectionString);
    }
);

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

//var connectionString = app.Configuration.GetSection("ConnectionStrings:").ToString();
//builder.Services.AddDbContext<ResearchesUFUContext>(options => options.UseNpgsql(connectionString));


app.Run();
