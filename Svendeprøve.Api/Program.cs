using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.Interface;
using Svendeprøve.Repo.Repository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// CORS-konfiguration for at tillade adgang fra frontend (localhost:7149)
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("http://localhost:7149")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});

// Controller-konfiguration med JSON-cyklusbeskyttelse
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repository-registrering og Dependency Injection
// Her registreres alle repositories via deres respektive interfaces. 
builder.Services.AddScoped<IMovie, MovieRepository>();
builder.Services.AddScoped<IHall, HallRepository>();
builder.Services.AddScoped<ISeat, SeatRepository>();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<ITicket, TicketRepository>();

// MovieRepository anvender eksternt HTTP-kald for TMDB API
builder.Services.AddHttpClient<IMovie, MovieRepository>();

// Gør konfigurationen tilgængelig i DI-containeren
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Tilføj databasekonfiguration
builder.Services.AddDbContext<Databasecontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Anvend CORS-politik
app.UseCors("CorsPolicy");

// Middleware-pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
