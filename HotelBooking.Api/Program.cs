using HotelBooking.Api.Infrastructure;
using HotelBooking.Api.Storage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------- Configuration ----------
var config = builder.Configuration;

// XML storage file path (fallback to App_Data/hotel.xml)
var xmlPath = config["XmlStorage:FilePath"]
              ?? Path.Combine(AppContext.BaseDirectory, "App_Data", "hotel.xml");

// ---------- Services ----------
builder.Services.AddControllers();

// Register XML storage (used by /api/xml/* controllers)
builder.Services.AddSingleton<IStorage>(_ => new XmlStorage(xmlPath));

// EF Core (demo: InMemory DB so /api/* controllers work out of the box)
// Swap to UseSqlServer(config.GetConnectionString("HotelDb")) if you add SQL Server.
builder.Services.AddDbContext<HotelContext>(opt =>
    opt.UseInMemoryDatabase("HotelDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ---------- Middleware & Swagger ----------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

// ---------- Seed EF demo data ----------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<HotelContext>();
    await DataSeeder.SeedAsync(db);
}

app.Run();
