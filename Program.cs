using QLab.Database;
using QLab.Database.Repositories;
using QLab.Helpers.DependncyInjection;
using QLab.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddDependencyInjection();

var app = builder.Build();
app.UseMiddleware<JwtMiddleware>();

if (app.Environment.IsDevelopment())
{
    var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
    var db = serviceScope?.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db?.Database.EnsureCreated();
    DataInitializer.Run(db);

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
