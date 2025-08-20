using HotelBooking.Api.Infrastructure; 
using HotelBooking.Api.Storage;          
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Config knobs
var repoMode = builder.Configuration.GetValue<string>("RepositoryMode")?.ToUpperInvariant() ?? "EF";
var sql = builder.Configuration.GetConnectionString("HotelDb");
var xmlPath = builder.Configuration.GetValue<string>("XmlFilePath") ?? "data/rooms.xml";

// Register repository
if (repoMode == "EF")
{
    builder.Services.AddDbContext<HotelContext>(opt => opt.UseSqlServer(sql));
}
else
{
    Directory.CreateDirectory(Path.GetDirectoryName(xmlPath)!);
    builder.Services.AddSingleton<IStorage>(new XmlStorage(xmlPath));
}

builder.Services.AddCors(opt => opt.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
var app = builder.Build();

app.UseCors();
app.MapControllers();
app.Run();
