using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.Interface;
using Svendeprøve.Repo.Repository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("http://localhost:7149")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IMovie, MovieRepository>();
builder.Services.AddScoped<IHall, HallRepository>();
builder.Services.AddScoped<ISeat, SeatRepository>();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<ITicket, TicketRepository>();


builder.Services.AddHttpClient<IMovie, MovieRepository>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Databasecontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(
    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.UseCors("CorsPolicy");

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
