var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var currentDirectory = Directory.GetCurrentDirectory();
//var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

//builder.Configuration
//    .SetBasePath(currentDirectory)
//                        .AddJsonFile("appsettings.json", false, true)
//                        .AddEnvironmentVariables();

//builder.Configuration.AddJsonFile($"appsettings.{env}.json", true, true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
