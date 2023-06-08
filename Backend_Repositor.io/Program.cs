using Backend_Repositor.io.Data;
using Backend_Repositor.io.Interfaces;
using Backend_Repositor.io.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins",
    builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IFileService, FileService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(@"Server=localhost;Database=Repositor.io;Trusted_Connection=true"));

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    appDbContext.Database.Migrate();
}

app.UseCors("AllowAngularOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
